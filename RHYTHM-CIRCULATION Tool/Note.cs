using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note
{
    public enum NoteType
    {
        NONE = 0,
        TAP,
        LONG,
        SLIDE,
        SHAKE,
    }

    public enum SlideWay
    {
        LEFT = 0,
        RIGHT,
    }

    class Note
    {
        public Note()
        {
        }
        ~Note()
        {
        }
    }
}
