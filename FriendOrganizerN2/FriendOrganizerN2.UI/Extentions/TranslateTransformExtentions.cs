using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace FriendOrganizerN2.UI.Extentions
{
    public static class TranslateTransformExtentions
    {
        public static void AnimateTo(this TranslateTransform translateTransform, Point point)
        {
            StartAnimation(translateTransform, TranslateTransform.XProperty, point.X);
            StartAnimation(translateTransform, TranslateTransform.YProperty, point.Y);
        }

        private static void StartAnimation(TranslateTransform translateTransform,
            DependencyProperty dependencyProperty, double toValue)
        {
            var animation = new DoubleAnimation
            {
                To = toValue,
                Duration = new Duration(TimeSpan.FromMilliseconds(500))
                //EasingFunction 
            };
            translateTransform.BeginAnimation(dependencyProperty, animation);
        }
    }
}
