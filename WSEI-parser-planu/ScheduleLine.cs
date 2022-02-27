using System;
using CsvHelper.Configuration.Attributes;

namespace WSEI_parser_planu {
    public class ScheduleLine {
        [Name("Subject")] public string subject { get; set; }
        [Name("Start Date")] public string startDate { get; set; }
        [Name("Start Time")] public string StartTime { get; set; }
        [Name("End Date")] public string endDate { get; set; }
        [Name("End Time")] public string EndTime { get; set; }
        [Name("Description")] public string description { get; set; }
        [Name("Location")] public string Room { get; set; }
      
      DateTime currentDate;
      string form;
      string @group;
      
        
        public ScheduleLine(DateTime currentDate, string startTime, string endTime, string subject, string form, string @group, string room) {
            this.currentDate = currentDate;
            startDate = endDate = currentDate.ToString("yyyy-MM-dd");
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.subject = $"\"{subject} {form} {@group}\"";
            this.form = form;
            this.@group = @group;
            this.Room = room;
        }

        public override string ToString() {
            return $"{currentDate:yyyy-MM-dd} {StartTime}-{EndTime} {subject} {form} {@group} {Room}";
        }
    }
}