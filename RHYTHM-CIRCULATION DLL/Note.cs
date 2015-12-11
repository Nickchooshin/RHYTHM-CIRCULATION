using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rhythm_Circulation
{
    public enum NoteType
    {
        NONE = 0,
        TAP,
        LONG,
        SLIDE,
        SNAP,
        LONG_SHADOW,
        SLIDE_SHADOW,
    }

    public enum NoteSlideWay
    {
        ANTI_CLOCKWISE = 0,
        CLOCKWISE,
    }

    public class NoteData
    {
        private NoteType m_type = NoteType.NONE;
        private int m_length = 0;
        private NoteSlideWay m_slideWay = NoteSlideWay.ANTI_CLOCKWISE;
        private bool m_roundTrip = false;

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

        public NoteSlideWay SlideWay
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

        public bool RoundTrip
        {
            get
            {
                return m_roundTrip;
            }
            set
            {
                m_roundTrip = value;
            }
        }
    }
}
