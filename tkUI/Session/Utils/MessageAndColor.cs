using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tkUI.Session.Utils
{
    class MessageAndColor
    {
        #region Fields

        string _message;
        string _color;

        #endregion // Fields

        #region Properties

        public string Message
        {
            get
            {
                if (_message == null)
                {
                    return "";
                }

                return _message;
            }

            set
            {
                _message = value;
            }
        }

        public string Color
        {
            get
            {
                if (_color == null)
                {
                    return "White";
                }

                return _color;
            }

            set
            {
                _color = value;
            }
            
        }

        #endregion // Properties
    }
}
