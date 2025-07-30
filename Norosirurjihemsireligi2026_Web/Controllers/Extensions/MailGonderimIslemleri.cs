using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace VeritabaniIslemMerkezi
{
    public class MailGonderimIslemleri
    {
        StringBuilder OutlookContent;
        string textCenter = "text-align: center;";

        public void KayitBilgilendirme(string OdemeID, string DilID)
        {
            KayitBilgilendirme(new OdemeTablosuIslemler().KayitBilgisi(OdemeID, DilID).Veriler, DilID);
        }


        public void KayitBilgilendirme(OdemeTablosuModel YeniKayit, string DilID = "tr")
        {
            using (MimeMessage mm = new MimeMessage())
            {
                mm.From.Add(new MailboxAddress("20.N�ro�ir�ji Hem�ireli�i Kongresi", "norosirurjihemsireligi2026@digiconkayit.com"));
                mm.To.Add(new MailboxAddress(YeniKayit.KatilimciBilgisi.AdSoyad, YeniKayit.KatilimciBilgisi.ePosta));
                mm.Bcc.Add(new MailboxAddress("20.N�ro�ir�ji Hem�ireli�i Kongresi", "tnd2026@honestglobal.com"));

                switch (DilID)
                {
                    default:
                    case "tr":
                        mm.Subject = "20.N�ro�ir�ji Hem�ireli�i Kongresi Kay�t Bilgilendirme";

                        OutlookContent = new StringBuilder()
                            .Append($"<p>Say�n {YeniKayit.KatilimciBilgisi.AdSoyad},</p>")
                            .Append($"<div><p>20.N�ro�ir�ji Hem�ireli�i Kongresine kay�t yapt�rd���n�z i�in te�ekk�r ederiz.</p>{(YeniKayit.OdemeTipiID.Equals(1) ? $"<p>�n kayd�n�z, �deme sonras� dekontunuzu iletti�iniz de onaylanacakt�r.</p><p><u><b>�deme tutar�, �deme yapt���n�z tarihteki d�nem �cretine g�re yap�lmal�d�r.</b></u> Aksi taktirde �n kayd�n�z onaylanmayacakt�r.</p><p>�demeyi, <u><b>sipari� numaras�</b></u>'n� dekontunuzda belirterek yapmay� unutmay�n�z. Dekontunuzu <a href=\"mailto:tnd2026@honestglobal.com\">tnd2026@honestglobal.com</a> adresine iletmeyi unutmay�n�z.</p><p></p>" : string.Empty)}<p>Kat�l�m bilgileriniz a�a��dad�r:</p></div>")
                            .Append("<table style=\"width:100%;\" border=\"1\">")
                            .Append($"<tr><td colspan=\"2\" style=\"{textCenter}\"><b>Ki�isel Bilgiler</b></td></tr>")
                            .Append($"<tr><td style=\"width:30%;\">Kimlik No</td><td>{YeniKayit.KatilimciBilgisi.KimlikNo}</td></tr>")
                            .Append($"<tr><td>Cinsiyet</td><td>{YeniKayit.KatilimciBilgisi.Cinsiyet}</td></tr>")
                            .Append($"<tr><td>e-Posta</td><td>{YeniKayit.KatilimciBilgisi.ePosta}</td></tr>")
                            .Append($"<tr><td>Cep Telefonu</td><td>{YeniKayit.KatilimciBilgisi.CepTelefonu}</td></tr>")
                            .Append($"<tr><td>Kurum</td><td>{YeniKayit.KatilimciBilgisi.Kurum}</td></tr>")
                            .Append($"<tr><td colspan=\"2\">&nbsp;</td></tr>")
                            .Append($"<tr><td colspan=\"2\" style=\"{textCenter}\"><b>Hizmetler</b></td></tr>")
                            .Append($"<tr><td>Kat�l�mc� Tipi</td><td>{YeniKayit.KatilimciBilgisi.KonaklamaBilgisi.KatilimciTipiOdaTipiBilgisi.KatilimciTipiBilgisi.KatilimciTipiDilBilgisi.First().KatilimciTipi}</td></tr>")
                            .Append($"<tr><td>Konaklama</td><td>{YeniKayit.KatilimciBilgisi.KonaklamaBilgisi.KatilimciTipiOdaTipiBilgisi.OdaTipiBilgisi.OtelBilgisi.Otel} / {YeniKayit.KatilimciBilgisi.KonaklamaBilgisi.KatilimciTipiOdaTipiBilgisi.OdaTipiBilgisi.OdaTipiDilBilgisi.First().OdaTipi}</td></tr>")
                            .Append($"<tr><td>Giri� Tarihi</td><td>{YeniKayit.KatilimciBilgisi.KonaklamaBilgisi.GirisTarihi:dd.MM.yyyy}</td></tr>")
                            .Append($"<tr><td>C�k�� Tarihi</td><td>{YeniKayit.KatilimciBilgisi.KonaklamaBilgisi.CikisTarihi:dd.MM.yyyy}</td></tr>")
                            ;

                        if (YeniKayit.KatilimciBilgisi.KonaklamaBilgisi.KatilimciTipiOdaTipiBilgisi.OdaTipiBilgisi.RefakatciDurum)
                        {
                            OutlookContent
                                .Append($"<tr><td>Refakat�i</td><td>{YeniKayit.KatilimciBilgisi.KonaklamaBilgisi.Refakatci}</td></tr>");
                        }

                        OutlookContent
                            .Append("<tr><td colspan=\"2\">&nbsp;</td></tr>")
                            .Append($"<tr><td colspan=\"2\" style=\"{textCenter}\"><b>�deme ve Fatura Bilgileri</b></td></tr>")
                            .Append($"<tr><td>Fatura Tipi</td><td>{YeniKayit.KatilimciBilgisi.FaturaTipi}</td></tr>")
                            .Append($"<tr><td>Fatura �nvan�</td><td>{YeniKayit.KatilimciBilgisi.FaturaUnvan}</td></tr>")
                            .Append($"<tr><td>Fatura Adresi</td><td>{YeniKayit.KatilimciBilgisi.FaturaAdres}</td></tr>")
                            .Append($"<tr><td>Vergi Dairesi</td><td>{YeniKayit.KatilimciBilgisi.VergiDairesi}</td></tr>")
                            .Append($"<tr><td>Vergi No</td><td>{YeniKayit.KatilimciBilgisi.VergiNo}</td></tr>")
                            .Append($"<tr><td>�deme Tipi</td><td>{YeniKayit.OdemeTipiBilgisi.OdemeTipiDilBilgisi.First().OdemeTipi}</td></tr>")
                            .Append($"<tr><td>Sipari� Numaras�</td><td>{YeniKayit.OdemeID}</td></tr>")
                            .Append($"<tr><td>Toplam</td><td>{YeniKayit.DovizUcret}</td></tr>");

                        if (YeniKayit.OdemeTipiID.Equals(1))
                        {
                            OutlookContent
                                .Append("<tr><td colspan=\"2\">&nbsp;</td></tr>")
                                .Append($"<tr><td colspan=\"2\" style=\"{textCenter}\"><b>Banka Havalesi Bilgisi</b></td></tr>")
                                .Append($"<tr><td>Banka</td><td> AKBANK </td></tr>")

                                .Append($"<tr><td>Hesap Ad�</td><td>  AKT�V TUR�ZM SEYEHAT VE KARGO TA�IMACILI�I </td></tr>")
                                .Append($"<tr><td>�ube</td><td>  HASANPA�A �UBE - 0235 </td></tr>")
                                .Append($"<tr><td>TL IBAN</td><td>  TR36 0004 6002 3588 8000 1624 39 </td></tr>")
                                .Append($"<tr><td>EURO IBAN</td><td>  TR80 0004 6002 3503 6000 1296 99 </td></tr>")
                                .Append($"<tr><td>A��klama</td><td>  ISTKONGRETND_2026</td></tr>");
                        }

                        OutlookContent
                            .Append("</table>");
                        break;

                    case "en":
                        mm.Subject = "ARC 2024 Regsitration Information";

                        OutlookContent = new StringBuilder()
                            .Append($"<p>Dear {YeniKayit.KatilimciBilgisi.AdSoyad},</p>")
                            .Append($"<div><p>Thank you for registering for the ARC Congress.</p>{(YeniKayit.OdemeTipiID.Equals(1) ? $"<p>Your pre-registration will be confirmed when you submit your receipt after payment.</p><p><u><b>The payment amount must be made according to the term fee on the date you make the payment.</b></u> Otherwise, your pre-registration will not be approved. p><p>Do not forget to make the payment by stating the <u><b>order number</b></u> on your receipt. Do not forget to send your receipt to <a href=\"mailto:...\">...@.........</a>.</p><p>&nbsp;</p>" : string.Empty)}<p>Your participation information is below:</p></div>")
                            .Append("<table style=\"width:100%;\" border=\"1\">")
                            .Append($"<tr><td colspan=\"2\" style=\"{textCenter}\"><b>Personal Information</b></td></tr>")
                            .Append($"<tr><td style=\"width:30%;\">Passaport No</td><td>{YeniKayit.KatilimciBilgisi.KimlikNo}</td></tr>")
                            .Append($"<tr><td>Gender</td><td>{YeniKayit.KatilimciBilgisi.Cinsiyet}</td></tr>")
                            .Append($"<tr><td>e-Mail</td><td>{YeniKayit.KatilimciBilgisi.ePosta}</td></tr>")
                            .Append($"<tr><td>Mobile Phone</td><td>{YeniKayit.KatilimciBilgisi.CepTelefonu}</td></tr>")
                            .Append($"<tr><td>Institution</td><td>{YeniKayit.KatilimciBilgisi.Kurum}</td></tr>")
                            .Append($"<tr><td colspan=\"2\">&nbsp;</td></tr>")
                            .Append($"<tr><td colspan=\"2\" style=\"{textCenter}\"><b>Services</b></td></tr>")
                            .Append($"<tr><td>Registration Type</td><td>{YeniKayit.KatilimciBilgisi.KonaklamaBilgisi.KatilimciTipiOdaTipiBilgisi.KatilimciTipiBilgisi.KatilimciTipiDilBilgisi.First().KatilimciTipi}</td></tr>")
                            .Append($"<tr><td>Accommoadtion</td><td>{YeniKayit.KatilimciBilgisi.KonaklamaBilgisi.KatilimciTipiOdaTipiBilgisi.OdaTipiBilgisi.OtelBilgisi.Otel} / {YeniKayit.KatilimciBilgisi.KonaklamaBilgisi.KatilimciTipiOdaTipiBilgisi.OdaTipiBilgisi.OdaTipiDilBilgisi.First().OdaTipi}</td></tr>")
                            .Append($"<tr><td>Check-In Date</td><td>{YeniKayit.KatilimciBilgisi.KonaklamaBilgisi.GirisTarihi:dd.MM.yyyy}</td></tr>")
                            .Append($"<tr><td>Check-Out Date</td><td>{YeniKayit.KatilimciBilgisi.KonaklamaBilgisi.CikisTarihi:dd.MM.yyyy}</td></tr>");

                        if (YeniKayit.KatilimciBilgisi.KonaklamaBilgisi.KatilimciTipiOdaTipiBilgisi.OdaTipiBilgisi.RefakatciDurum)
                        {
                            OutlookContent
                                .Append($"<tr><td>Accommpaying' Person</td><td>{YeniKayit.KatilimciBilgisi.KonaklamaBilgisi.Refakatci}</td></tr>");
                        }

                        OutlookContent
                            .Append($"<tr><td>Transfer</td><td>{YeniKayit.KatilimciBilgisi.TransferBilgisi.TransferTipiBilgisi.TransferTipiDilBilgisi.First().TransferTipi}</td></tr>")
                            .Append($"<tr><td>Course(s)</td><td>{string.Join(", ", (YeniKayit.KatilimciBilgisi.KatilimciKursBilgisi is null ? new List<string>() : YeniKayit.KatilimciBilgisi.KatilimciKursBilgisi.Select(x => x.KursTipiBilgisi.KursTipiDilBilgisi.First().KursTipi).ToList()))}</td></tr>")
                            .Append($"<tr><td>Event(s)</td><td>{string.Join(", ", (YeniKayit.KatilimciBilgisi.KatilimciEtkinlikBilgisi is null ? new List<string>() : YeniKayit.KatilimciBilgisi.KatilimciEtkinlikBilgisi.Select(x => x.EtkinlikBilgisi.EtkinlikDilBilgisi.First().Etkinlik).ToList()))}</td></tr>")
                            .Append("<tr><td colspan=\"2\">&nbsp;</td></tr>")
                            .Append($"<tr><td colspan=\"2\" style=\"{textCenter}\"><b>Payment & Invoice Informations</b></td></tr>")
                            .Append($"<tr><td>Invoice Type</td><td>{YeniKayit.KatilimciBilgisi.FaturaTipi}</td></tr>")
                            .Append($"<tr><td>Invoice Title</td><td>{YeniKayit.KatilimciBilgisi.FaturaUnvan}</td></tr>")
                            .Append($"<tr><td>Invoice Address</td><td>{YeniKayit.KatilimciBilgisi.FaturaAdres}</td></tr>")
                            .Append($"<tr><td>Tax Office</td><td>{YeniKayit.KatilimciBilgisi.VergiDairesi}</td></tr>")
                            .Append($"<tr><td>Tax No</td><td>{YeniKayit.KatilimciBilgisi.VergiNo}</td></tr>")
                            .Append($"<tr><td>Payment Type</td><td>{YeniKayit.OdemeTipiBilgisi.OdemeTipiDilBilgisi.First().OdemeTipi}</td></tr>")
                            .Append($"<tr><td>Order No</td><td>{YeniKayit.OdemeID}</td></tr>")
                            .Append($"<tr><td>Total</td><td>{YeniKayit.DovizUcret}</td></tr>");

                        if (YeniKayit.OdemeTipiID.Equals(1))
                        {
                            OutlookContent
                                .Append("<tr><td colspan=\"2\">&nbsp;</td></tr>")
                                .Append($"<tr><td colspan=\"2\" style=\"{textCenter}\"><b>Bank Transfer Information</b></td></tr>")
                                .Append($"<tr><td>Hesap Ad�</td><td> *** </td></tr>")
                                .Append($"<tr><td>Banka</td><td> *** </td></tr>")
                                .Append($"<tr><td>�ube</td><td> *** </td></tr>")
                                .Append($"<tr><td>Hesap No</td><td> *** </td></tr>")
                                .Append($"<tr><td>IBAN</td><td> *** </td></tr>");
                        }

                        OutlookContent
                            .Append("</table>");
                        break;
                }



                mm.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    ContentTransferEncoding = ContentEncoding.Base64,
                    Text = OutlookContent.ToString()
                };

                using (ProtocolLogger logger = new ProtocolLogger(HttpContext.Current.Server.MapPath($"~/Dosyalar/MailLog/{YeniKayit.KatilimciBilgisi.ePosta}_{DateTime.Now:yyyy.MM.dd HH.mm.ss}.log")))
                {
                    using (SmtpClient client = new SmtpClient(logger))
                    {
                        try
                        {
                            client.Connect("mail.digiconkayit.com", 587, MailKit.Security.SecureSocketOptions.None);
                            client.AuthenticationMechanisms.Remove("DIGEST-MD5");
                            client.Authenticate("norosirurjihemsireligi2026@digiconkayit.com", "eaQ-g8fh");

                            client.Send(mm);
                            client.Disconnect(true);
                        }
                        catch (Exception ex)
                        {
                            byte[] exToArray = Encoding.UTF8.GetBytes(ex.Message);
                            logger.Stream.Write(exToArray, 0, exToArray.Length);
                            client.Disconnect(false);
                        }
                    }
                }
            }
        }
    }
}
