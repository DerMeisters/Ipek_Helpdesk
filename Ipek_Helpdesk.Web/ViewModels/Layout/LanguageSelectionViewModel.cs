﻿using System.Collections.Generic;
using Abp.Localization;

namespace Ipek_Helpdesk.Web.Models.Layout
{
    public class LanguageSelectionViewModel
    {
        public LanguageInfo CurrentLanguage { get; set; }

        public IReadOnlyList<LanguageInfo> Languages { get; set; }
    }
}