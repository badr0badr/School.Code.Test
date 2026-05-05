﻿using System;
using System.Linq;

namespace Application.Core.Helper
{
    public static class DateHelper
    {
        public static int GetYear(DateTime date) => date.Year;
        public static int GetMonth(DateTime date) => date.Month;
        public static int GetDay(DateTime date) => date.Day;
        public static string GetDayName(DateTime date) => date.ToString("dddd");
        public static string GetDateString(DateTime date) => date.ToString("dd/MM/yyyy");
        public static string GetTimeString(DateTime date) => date.ToString("HH:mm:ss");
        public static string? GetStringFromDate(DateTime? date)
        {
            if (date != null)
                return date.Value.ToString("dd/MM/yyyy");
            return null;
        }
        public static string GetDateTimeString(DateTime date) => date.ToString("dd/MM/yyyy HH:mm:ss");
        public static string GetDateTimeString(DateTime date, string format) => date.ToString(format);
        public static string MonthNameEng(int month)
        {
            return month switch
            {
                1 => "January",
                2 => "February",
                3 => "March",
                4 => "April",
                5 => "May",
                6 => "June",
                7 => "July",
                8 => "August",
                9 => "September",
                10 => "October",
                11 => "November",
                12 => "December",
                _ => "Wrong Month"
            };
        }
        public static string MonthNameAr(int month)
        {
            return month switch
            {
                1 => "يناير",
                2 => "فبراير",
                3 => "مارس",
                4 => "أبريل",
                5 => "مايو",
                6 => "يونيو",
                7 => "يوليو",
                8 => "أغسطس",
                9 => "سبتمبر",
                10 => "أكتوبر",
                11 => "نوفمبر",
                12 => "ديسمبر",
                _ => "شهر خطأ"
            };
        }
        public static string DayNameAr(string day)
        {
            return day.ToLower() switch
            {
                "saturday" => "السبت",
                "sunday" => "الأحد",
                "monday" => "الإثنين",
                "tuesday" => "الثلاثاء",
                "wednesday" => "الأربعاء",
                "thursday" => "الخميس",
                "friday" => "الجمعة",
                _ => "يوم خطأ"
            };
        }
    }
}