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
        LONG_SHADOW,
        SLIDE_SHADOW,
    }

    public enum SlideWay
    {
        ANTI_CLOCKWISE = 0,
        CLOCKWISE,
    }

    class NoteData
    {
        private NoteType m_type = NoteType.NONE;
        private int m_length = 0;
        private SlideWay m_slideWay = SlideWay.ANTI_CLOCKWISE;

        public NoteData()
        {
        }
        ~NoteData()
        {
        }

        public NoteType Type
        {
            get
            {
                return m_type;
            }
            set
            {
                m_type = value;
            }
        }

        public int Length
        {
            get
            {
                return m_length;
            }
            set
            {
                m_length = value;
            }
        }

        public SlideWay SlideWay
        {
            get
            {
                return m_slideWay;
            }
            set
            {
                m_slideWay = value;
            }
        }
    }
}
