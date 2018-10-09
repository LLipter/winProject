using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.International.Converters.PinYinConverter;
using Microsoft.International.Converters.TraditionalChineseToSimplifiedConverter;
using DotNetSpeech;


namespace CHSCHTPinYinConv
{
    public partial class frmCHSCHTPinYinConv : Form
    {
        public frmCHSCHTPinYinConv()
        {
            InitializeComponent();
        }

        private void btnToCHT_Click(object sender, EventArgs e)
        {
            txtCHT.Text = ChineseConverter.Convert(txtCHS.Text.Trim(), ChineseConversionDirection.SimplifiedToTraditional);

        }

        private void btnPinyin_Click(object sender, EventArgs e)
        {
            string chs = txtCHS.Text.Trim();
            if (chs.Length == 0)
                return;

            StringBuilder fullSpell = new StringBuilder();

            foreach (char c in chs)
            {
                if (ChineseChar.IsValidChar(c))
                {
                    ChineseChar chschar = new ChineseChar(c);
                    string pin = chschar.Pinyins[0];
                    pin = pin.Remove(pin.Length - 1);
                    fullSpell.Append(pin);
                    fullSpell.Append(" ");
                }
                else
                {
                    fullSpell.Append(c);
                    fullSpell.Append(" ");
                }
            }

            lblPinyin.Text = fullSpell.ToString();
        }

        private void btnToCHS_Click(object sender, EventArgs e)
        {
            txtCHS.Text = ChineseConverter.Convert(txtCHT.Text.Trim(), ChineseConversionDirection.TraditionalToSimplified);

        }

        private void btnVoice_Click(object sender, EventArgs e)
        {
            SpVoice voice = new SpVoice();
            SpeechVoiceSpeakFlags spflags = SpeechVoiceSpeakFlags.SVSFDefault;
            voice.Speak(txtCHS.Text.Trim(), spflags);
        }
    }
}
