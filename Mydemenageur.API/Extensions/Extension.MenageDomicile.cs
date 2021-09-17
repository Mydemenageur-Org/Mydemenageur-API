﻿using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Services;
using Access = Mydemenageur.API.Entities;

namespace Mydemenageur.API.Extensions
{
    static partial class Extension
    {
        public static MenageDomicileModel ToModelMen(this Access.MenageDomicile men)
        {
            if (men != null)
            {
                return GetMen(men);
            }

            return null;
        }

        private static MenageDomicileModel GetMen(MenageDomicile men)
        {
            return new MenageDomicileModel()
            {
                Menage = men.Menage,
                Surface = men.Surface,
                AdditionnalNeeds = men.AdditionnalNeeds,
                Frequency = men.Frequency
            };
        }
    }
}
