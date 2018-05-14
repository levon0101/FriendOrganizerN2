using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizerN2.UI.Event
{
    public class AfterDetailClosedEvent:PubSubEvent<AfterDetailClosedEventAegs>
    {

    }
    public class AfterDetailClosedEventAegs
    {
        public int Id { get; set; }
        public string ViewModelName { get; set; }
    }
}
