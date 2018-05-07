using FriendOrganizer.Model;
using System;
using System.Collections.Generic;

namespace FriendOrganizerN2.UI.Wrapper
{

    public class FriendWrapper : ModelWrapper<Friend>
    {
        public FriendWrapper(Friend model) : base(model)
        {
        }

        public int Id { get { return Model.Id; } }

        public string FirstName
        {
            get => GetValue<string>();
            set
            { SetValue<string>(value); }
        }


        public string LastName
        {
            get => GetValue<string>();
            set
            {
                SetValue<string>(value);
            }
        }

        public string Email
        {
            get => GetValue<string>();
            set
            {
                SetValue<string>(value);
            }
        }

        public int? FavoriteLanguageId
        {
            get => GetValue<int?>();
            set
            {
                SetValue(value);
            }
        }

        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(FirstName):
                    if (string.Equals(FirstName, "Robot", StringComparison.OrdinalIgnoreCase))
                    {
                        yield return "Name can't be robot";
                    }
                    break;
            }
        }
    }
}
