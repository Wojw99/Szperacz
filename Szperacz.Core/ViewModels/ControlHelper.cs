using System;
using System.Collections.Generic;
using System.Text;

namespace Szperacz.Core.ViewModels
{
    public static class ControlHelper
    {
        public static string WordToFind { get; set; } = "pomidor";
    }

    enum MessageType
    {
        PhraseIncorrect,
        PathIncorrect,
        PhraseNotFound
    }
}
