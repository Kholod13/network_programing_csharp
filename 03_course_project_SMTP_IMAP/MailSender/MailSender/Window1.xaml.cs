using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MailSender
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private string UserLogin { get; set; }
        private string UserPassword { get; set; }

        private List<string> FileNames { get; set; } = new();

        MailMessage mail = new MailMessage();
        public Window1(string userLogin, string userPassword)
        {
            InitializeComponent();
            UserLogin= userLogin;
            UserPassword= userPassword;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // create new mail
            mail = new MailMessage(UserLogin, toTxtBox.Text)
            {
                Subject = subjectTxtBox.Text,
                Body = $"<h1>Mail Message from {UserLogin}</h1><p>{bodyTxtBox.Text}</p>",
                IsBodyHtml = true,
                Priority = MailPriority.High
            };


            foreach (var i in FileNames)
            {
                mail.Attachments.Add(new Attachment(i));

            }
            

            

            // send mail message
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(UserLogin, UserPassword),
                EnableSsl = true
            };

            client.Send(mail);
            subjectTxtBox.Clear();
            bodyTxtBox.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
                //mail.Attachments.Add(new Attachment(dialog.FileName));
                FileNames.Add(dialog.FileName);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MailInteface w = new MailInteface(UserLogin, UserPassword);
            w.Show();
            this.Close();
        }
    }
}
