using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using demo0312.Models;

//using demo0312.Models;
using MsBox.Avalonia;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace demo0312;

public partial class Zayavka : Window
{
    public Bitmap bitm;
    public string path;
    public string filename;
    public string destPath;
    public List<string> podrazd = Helper.context.Departments.Select(x => x.Dep).ToList();
    public List<string> aims = Helper.context.Purposes.Select(x => x.Purpose1).ToList();
    public List<Employee1> employees = Helper.context.Employee1s.ToList();
    public Zayavka()
    {
        InitializeComponent();
        img.Source= new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "Assets\\zaglushka.png");
        aimCB.ItemsSource = aims;
        podrCB.ItemsSource = podrazd;

    }

    private void vyhod_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        CurrentZayavki currentzayavki = new CurrentZayavki();
        currentzayavki.Show();
        this.Close();
    }


    private void Ok_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        bool check = true;
        DateOnly srokNach = DateOnly.FromDateTime((DateTime)nachD.SelectedDate.Value.DateTime);
        DateOnly srokKon = DateOnly.FromDateTime((DateTime)endD.SelectedDate.Value.DateTime);
        Order zayavka = new Order();
        if (Helper.context.Orders.Count() > 0)
        {
            zayavka.IdOrder = Helper.context.Orders.OrderBy(x => x.IdOrder).Last().IdOrder + 1;
        }
        else
        {
            zayavka.IdOrder = 1;
        }
        zayavka.IdPurposeNavigation = Helper.context.Purposes.Where(x => x.Purpose1 == aimCB.SelectedValue.ToString()).First();
        zayavka.IdPurpose = Helper.context.Purposes.Where(x => x.Purpose1 == aimCB.SelectedValue.ToString()).First().IdPurpose;
        if (DateTime.Now.Date.AddDays(1) <= (DateTime)nachD.SelectedDate.Value.DateTime
            && DateTime.Now.Date.AddDays(15) >= (DateTime)nachD.SelectedDate.Value.DateTime && check == true)
        {
            zayavka.DateNach = srokNach;
        }
        else
        {
            check = false;
            var ms = MessageBoxManager.GetMessageBoxStandard("������", "����������� �������� ���� ������ �������� ������ - ��������� ���� �� ������� ����, ������������ - 15 ���� �� �������� ���");
            ms.ShowAsync();
        }
        if ((DateTime)nachD.SelectedDate.Value.DateTime <= (DateTime)endD.SelectedDate.Value.DateTime &&
            (DateTime)nachD.SelectedDate.Value.DateTime.AddDays(15) >= (DateTime)endD.SelectedDate.Value.DateTime && check == true)
        {
            zayavka.DateEnd = srokKon;
        }
        else
        {
            check = false;
            var ms = MessageBoxManager.GetMessageBoxStandard("������", "����������� �������� - ��������� ���� �� ���� ������, ������������ - 15 ���� �� ���� ������ �����");
            ms.ShowAsync();
        }
        if (sotrCB.SelectedItem != null && check == true)
        {
            string fio = sotrCB.SelectedItem.ToString();
            zayavka.IdEmployeeNavigation = Helper.context.Employee1s.Where(x => x.Username + " " + x.Userpatronymic + " " + x.Usersurname == fio).First();
            zayavka.IdEmployee = Helper.context.Employee1s.Where(x => x.Username + " " + x.Userpatronymic + " " + x.Usersurname == fio).First().Userid;
        }
        else
        {
            check = false;
            var ms = MessageBoxManager.GetMessageBoxStandard("������", "�� ������ ���������");
            ms.ShowAsync();
            this.Close();
        }
        
      
        
        switch (check)
        {
            case true:
                zayavka.IdStatusNavigation = Helper.context.Statuses.Where(x => x.IdStatus == 1).First();
                zayavka.IdStatus = 1;
                Visitor vis = new Visitor();
                vis.IdVisitor = Helper.context.Visitors.OrderBy(x => x.IdVisitor).Last().IdVisitor + 1;
                vis.Usersurname = surn.Text;
                vis.Username = name.Text;
                vis.Userpatronymic = otch.Text;
                vis.SeriyaPassp = seriaMask.Text;
                vis.NomPassp = nomerMask.Text;
                vis.Dateofbirth = birthDP.SelectedDate.ToString();
                vis.Organization = orgInp.Text;
                vis.Phone = phoneMask.Text;
                vis.Primechanie = primechanieInp.Text;
                Helper.context.Visitors.Add(vis);
                Helper.context.SaveChanges();
                if (Helper.curruser != null)
                {
                    zayavka.IdUserNavigation = Helper.context.Users.Where(x => x.IdUser == Helper.curruser.IdUser).First();
                    zayavka.IdUser = Helper.curruser.IdUser;
                }
                zayavka.IdVisitorNavigation = Helper.context.Visitors.Where(x => x.IdVisitor == vis.IdVisitor).First();
                zayavka.IdVisitor = vis.IdVisitor;
                Helper.context.Orders.Add(zayavka);
                Helper.context.SaveChanges();
                SelectZayavka sel = new SelectZayavka();
                sel.Show();
                this.Close();
                break;
            case false:
                var ms = MessageBoxManager.GetMessageBoxStandard("������", "��������� � �������� ��������� ������");
                ms.ShowAsync();
                break;
        }
    }

    private void AddFile_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {



    }

    private void ComboBox_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    {
        Department podr = Helper.context.Departments.Where(x => x.Dep == podrCB.SelectedItem.ToString()).First();
        List<string> fios = Helper.context.Employee1s.Where(x => x.IdDep == podr.IdDep).Select(x => x.Username + " " + x.Userpatronymic + " " + x.Usersurname).ToList();

        sotrCB.ItemsSource = fios;

    }

    private async void img_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        OpenFileDialog op = new OpenFileDialog();
        var res = await op.ShowAsync(this);
        if (res == null) return;
        path = string.Join("", res);
        if (res != null)
        {
            bitm = new Bitmap(path);
        }
        img.Source = bitm;
        filename = Path.GetFileName(path);
        destPath = Path.Combine(path, AppDomain.CurrentDomain.BaseDirectory + "\\Assets");

    }
}