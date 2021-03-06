﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNotifier.View
{
    public class AppSettings
    {
        //Time is in hours
        public static int ValidateRestartTime { get; set; } = int.Parse((ConfigurationManager.AppSettings["ValidateRestartTime"] != null ? ConfigurationManager.AppSettings["ValidateRestartTime"].ToString() : "24"));

        //Time is in hours
        public static int ValidateRepeateNotificationTime { get; set; } = int.Parse((ConfigurationManager.AppSettings["ValidateRepeateNotificationTime"] != null ? ConfigurationManager.AppSettings["ValidateRepeateNotificationTime"].ToString() : "24"));

        //Threshold Space For Notification Alert 
        //Giga Bytes
        public static int ThresholdSpaceForNotificationAlert { get; set; } = int.Parse((ConfigurationManager.AppSettings["ThresholdSpaceForNotificationAlert"] != null ? ConfigurationManager.AppSettings["ThresholdSpaceForNotificationAlert"].ToString() : "20"));

        //DB Refresh Interval 
        //In Seconds
        public static int DBRefreshInterval { get; set; } = int.Parse((ConfigurationManager.AppSettings["DBRefreshInterval"] != null ? ConfigurationManager.AppSettings["DBRefreshInterval"].ToString() : "15"));
    }
}
