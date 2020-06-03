using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Varesin.Database;
using Varesin.Mvc.Mapping;
using Varesin.Mvc.Models.Info;
using DNTPersianUtils.Core;

namespace Varesin.Mvc.Services
{
    public class InfoService
    {
        private readonly AppDbContext _context;
        public InfoService(AppDbContext context)
        {
            _context = context;
        }
        public InfoViewModel Get()
        {
            var data = _context.Infoes.ToList();
            return data.ToViewModel();
        }
        public string GetPhoneNumbers(InfoViewModel model)
        {
            string result = "";

            var array = new List<string>();

            if (!string.IsNullOrEmpty(model.PhoneNumber1))
                array.Add(model.PhoneNumber1);

            if (!string.IsNullOrEmpty(model.PhoneNumber2))
                array.Add(model.PhoneNumber2);

            if (!string.IsNullOrEmpty(model.PhoneNumber3))
                array.Add(model.PhoneNumber3);

            if (!string.IsNullOrEmpty(model.PhoneNumber4))
                array.Add(model.PhoneNumber4);

            for (int i = 0; i < array.Count; i++)
            {
                result += $"{array[i]}";
                if (i != array.Count - 1)
                    result += " - ";
            }

            return result.ToPersianNumbers();
        }
    }
}
