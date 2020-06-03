using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Varesin.Domain.Entities;
using Varesin.Domain.Enumeration;
using Varesin.Mvc.Models.Info;

namespace Varesin.Mvc.Mapping
{
    public static class InfoMapping
    {
        public static List<Info> ToEntity(this InfoViewModel source)
        {
            var result = new List<Info>();

            result.Add(new Info
            {
                Name = "اکانت بله",
                Value = source.BaleAccount,
                Type = Domain.Enumeration.InfoType.BaleAccount
            });

            result.Add(new Info
            {
                Name = "اکانت سروش",
                Value = source.SoroushAccount,
                Type = Domain.Enumeration.InfoType.SoroushAccount
            });

            result.Add(new Info
            {
                Name = "اکانت تلگرام",
                Value = source.TelegramAccount,
                Type = Domain.Enumeration.InfoType.TelegramAccount
            });

            result.Add(new Info
            {
                Name = "شماره تلفن 1",
                Value = source.PhoneNumber1,
                Type = Domain.Enumeration.InfoType.PhoneNumber1
            });

            result.Add(new Info
            {
                Name = "شماره تلفن 2",
                Value = source.PhoneNumber2,
                Type = Domain.Enumeration.InfoType.PhoneNumber2
            });

            result.Add(new Info
            {
                Name = "شماره تلفن 3",
                Value = source.PhoneNumber3,
                Type = Domain.Enumeration.InfoType.PhoneNumber3
            });

            result.Add(new Info
            {
                Name = "شماره تلفن 4",
                Value = source.PhoneNumber4,
                Type = Domain.Enumeration.InfoType.PhoneNumber4
            });

            result.Add(new Info
            {
                Name = "اکانت بیت پی",
                Password = source.BitpayPassword,
                UserName = source.BitpayUserName,
                Type = Domain.Enumeration.InfoType.Bitpay
            });

            result.Add(new Info
            {
                Name = "اکانت اینستاگرام",
                Password = source.InstagramPassword,
                UserName = source.InstagramUserName,
                Type = Domain.Enumeration.InfoType.InstagramAccount
            });

            result.Add(new Info
            {
                Name = "آدرس",
                Value = source.Address,
                Type = Domain.Enumeration.InfoType.Address
            });

            result.Add(new Info
            {
                Name = "درباره ما",
                Value = source.AboutUs,
                Type = Domain.Enumeration.InfoType.AboutUs
            });

            result.Add(new Info
            {
                Name = "اکانت اینستاگرام فضای مجازی",
                Value = source.InstagramAccount,
                Type = Domain.Enumeration.InfoType.InstaAccount
            });

            return result;
        }

        public static InfoViewModel ToViewModel(this List<Info> sources)
        {
            var result = new InfoViewModel();

            result.AboutUs = sources.FirstOrDefault(c => c.Type == InfoType.AboutUs)?.Value;
            result.Address = sources.FirstOrDefault(c => c.Type == InfoType.Address)?.Value;
            result.BaleAccount = sources.FirstOrDefault(c => c.Type == InfoType.BaleAccount)?.Value;
            result.InstagramAccount = sources.FirstOrDefault(c => c.Type == InfoType.InstaAccount)?.Value;
            result.PhoneNumber1 = sources.FirstOrDefault(c => c.Type == InfoType.PhoneNumber1)?.Value;
            result.PhoneNumber2 = sources.FirstOrDefault(c => c.Type == InfoType.PhoneNumber2)?.Value;
            result.PhoneNumber3 = sources.FirstOrDefault(c => c.Type == InfoType.PhoneNumber3)?.Value;
            result.PhoneNumber4 = sources.FirstOrDefault(c => c.Type == InfoType.PhoneNumber4)?.Value;
            result.SoroushAccount = sources.FirstOrDefault(c => c.Type == InfoType.SoroushAccount)?.Value;
            result.TelegramAccount = sources.FirstOrDefault(c => c.Type == InfoType.TelegramAccount)?.Value;

            var bitPay = sources.FirstOrDefault(c => c.Type == InfoType.Bitpay);
            result.BitpayPassword = bitPay?.Password;
            result.BitpayUserName = bitPay?.UserName;

            var instagram = sources.FirstOrDefault(c => c.Type == InfoType.InstagramAccount);
            result.InstagramPassword = instagram?.Password;
            result.InstagramUserName = instagram?.UserName;

            return result;
        }
    }
}
