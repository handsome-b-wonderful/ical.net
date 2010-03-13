using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Serialization;

namespace DDay.iCal
{    
    /// <summary>
    /// A class that contains time zone information, and is usually accessed
    /// from an iCalendar object using the <see cref="DDay.iCal.iCalendar.GetTimeZone"/> method.        
    /// </summary>
#if DATACONTRACT
    [DataContract(Name = "iCalTimeZoneInfo", Namespace = "http://www.ddaysoftware.com/dday.ical/2009/07/")]
#endif
    [Serializable]
    public class iCalTimeZoneInfo : 
        RecurringComponent,
        ITimeZoneInfo
    {
        #region ITimeZoneInfo Members

        /// <summary>
        /// Returns the name of the current Time Zone.
        /// <example>
        ///     The following are examples:
        ///     <list type="bullet">
        ///         <item>EST</item>
        ///         <item>EDT</item>
        ///         <item>MST</item>
        ///         <item>MDT</item>
        ///     </list>
        /// </example>
        /// </summary>
        virtual public string TimeZoneName
        {
            get 
            {
                IList<string> tzNames = TimeZoneNames;
                if (tzNames != null &&
                    tzNames.Count > 0)
                    return tzNames[0];
                return null;
            }
            set
            {
                IList<string> tzNames = TimeZoneNames;
                if (tzNames != null &&
                    tzNames.Count > 0)
                    tzNames[0] = value;
                else
                {
                    if (value != null)
                        tzNames = new List<string>(new string[] { value });
                    else
                        tzNames = null;
                }
            }
        }

        virtual public IUTCOffset TZOffsetFrom
        {
            get { return OffsetFrom; }
            set { OffsetFrom = value; }
        }

        virtual public IUTCOffset OffsetFrom
        {
            get { return Properties.Get<IUTCOffset>("TZOFFSETFROM"); }
            set { Properties.Set("TZOFFSETFROM", value); }
        }

        virtual public IUTCOffset OffsetTo
        {
            get { return Properties.Get<IUTCOffset>("TZOFFSETTO"); }
            set { Properties.Set("TZOFFSETTO", value); }
        }

        virtual public IUTCOffset TZOffsetTo
        {
            get { return OffsetTo; }
            set { OffsetTo = value; }
        }

        virtual public IList<string> TimeZoneNames
        {
            get { return Properties.GetList<string>("TZNAME"); }
            set { Properties.SetList<string>("TZNAME", value); }
        }

        #endregion

        #region Constructors

        public iCalTimeZoneInfo() : base()
        {
            // FIXME: how do we ensure SEQUENCE doesn't get serialized?
            //base.Sequence = null; // iCalTimeZoneInfo does not allow sequence numbers
        }
        public iCalTimeZoneInfo(string name) : this()
        {
            this.Name = name;
        }

        #endregion

        #region Overrides

        public override bool Equals(object obj)
        {
            iCalTimeZoneInfo tzi = obj as iCalTimeZoneInfo;
            if (tzi != null)
            {
                return object.Equals(TimeZoneName, tzi.TimeZoneName) &&
                    object.Equals(OffsetFrom, tzi.OffsetFrom) &&
                    object.Equals(OffsetTo, tzi.OffsetTo);
            }
            return base.Equals(obj);
        }
                              
        #endregion
    }    
}