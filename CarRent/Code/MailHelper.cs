
using System;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mail;
using System.Net.Mail;
using Codes;
using System.Web.Mail;
public class MailHelper
{
	/// <summary>
	/// This class file use for mailing service with attachment.
	/// <summary>
	public static bool SendMailMessage(System.String To, System.String Bcc, System.String Cc, System.String Subject, System.String Message, string FileName = null)
	{

		if (
			string.IsNullOrEmpty(To) ||
			string.IsNullOrEmpty(Subject) ||
			string.IsNullOrEmpty(Message)
			)
			return false;


		System.String From = ConfigurationWrapper.SMTP_FROM;

		try
		{
			SendNetMail(From, To, Bcc, Cc, Subject, Message, FileName);
			return true;
		}
		catch
		{
			try
			{
				SendWebMail(From, To, Bcc, Cc, Subject, Message, FileName);
				return true;
			}
			catch
			{
				try
				{
                    var MM = new System.Web.Mail.MailMessage()
                    {
                        From = From,
                        Subject = Subject,
                        To = To
                    };
                    if (!string.IsNullOrEmpty(Cc))
						MM.Cc = Cc;
					if (!string.IsNullOrEmpty(Bcc))
						MM.Bcc = Bcc;

					if (!string.IsNullOrEmpty(FileName))
					{
						System.Web.Mail.MailAttachment _mailAttachment = new MailAttachment(FileName);
						MM.Attachments.Add(_mailAttachment);

					}

					MM.Body = Message;
					MM.BodyFormat = System.Web.Mail.MailFormat.Html;
					System.Web.Mail.SmtpMail.Send(MM);
					return true;
				}
				catch
				{
					return false;
				}
			}
		}

	}

	/// <summary>
	/// This class file use for optional mailing service.
	/// <summary>
	private static void SendWebMail(System.String from, System.String to, System.String bcc, System.String cc, System.String subject, System.String body, string FileName = null)
	{
		System.Web.Mail.MailMessage mail = new System.Web.Mail.MailMessage();
		mail.From = from;
		mail.Subject = subject;
		mail.To = to;
		if (!string.IsNullOrEmpty(cc))
			mail.Cc = cc;
		if (!string.IsNullOrEmpty(bcc))
			mail.Bcc = bcc;

		if (!string.IsNullOrEmpty(FileName))
		{
			System.Web.Mail.MailAttachment _mailAttachment = new MailAttachment(FileName);
			mail.Attachments.Add(_mailAttachment);
		}

		mail.Body = body;
		mail.BodyFormat = System.Web.Mail.MailFormat.Html;
		mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");    //basic authentication
		mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", ConfigurationWrapper.SMTP_USER); //set your username here
		mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", ConfigurationWrapper.SMTP_PASSWORD);    //set your password here
		System.Web.Mail.SmtpMail.SmtpServer = ConfigurationWrapper.SMTP_SERVER;  //your real server goes here
		System.Web.Mail.SmtpMail.Send(mail);
	}

	/// <summary>
	/// This class file use for optional mailing service.
	/// <summary>
	private static void SendNetMail(System.String from, System.String to, System.String bcc, System.String cc, System.String subject, System.String body, string FileName = null)
	{
		using (System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage())
		{
			msg.From = new System.Net.Mail.MailAddress(from);
			msg.Subject = subject;
			msg.SubjectEncoding = System.Text.Encoding.UTF8;
			msg.To.Add(to);
			if (!string.IsNullOrEmpty(cc))
				msg.CC.Add(cc);
			if (!string.IsNullOrEmpty(bcc))
				msg.Bcc.Add(bcc);
			msg.Body = body;
			msg.BodyEncoding = System.Text.Encoding.UTF8;
			msg.IsBodyHtml = true;
			msg.Priority = System.Net.Mail.MailPriority.High;

			if (!string.IsNullOrEmpty(FileName))
			{
				System.Net.Mail.Attachment _mailAttachment = new Attachment(FileName);
				msg.Attachments.Add(_mailAttachment);
			}

			System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
			client.EnableSsl = true;
			client.Credentials = new System.Net.NetworkCredential
				(
				ConfigurationWrapper.SMTP_USER,
				ConfigurationWrapper.SMTP_PASSWORD
				);
			client.Port = ConfigurationWrapper.SMTP_PORT;
			client.Host = ConfigurationWrapper.SMTP_SERVER;
			client.Send(msg);
		}
	}
}
