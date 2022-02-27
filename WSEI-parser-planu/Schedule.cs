using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using CsvHelper;
using HtmlAgilityPack;

namespace WSEI_parser_planu {
    public class Schedule {
        public List<ScheduleLine> lines = new List<ScheduleLine>();

        public Schedule(string scheduleHtml) {
            var doc = new HtmlDocument();
            doc.LoadHtml(scheduleHtml);

            var table = doc.DocumentNode.SelectNodes("//*[@id='gridViewPlanProwadzacego_DXMainTable']/tbody/*");
            Console.WriteLine($"Wczytano {table.Count} wierszy!");
            AddData(table);
        }

        DateTime currentDate;

        void AddData(HtmlNodeCollection rows) {
            foreach (var row in rows) {
                if (IsDateRow(row))
                    UpdateCurrentDate(row);
                else if (IsLessonRow(row)) {
                    var fields = row.SelectNodes("(./*)");
                    var czas_od = DeHTML(fields[1].InnerText);
                    var czas_do = DeHTML(fields[2].InnerText);
                    var subject = DeHTML(fields[4].InnerText);
                    var forma = DeHTML(fields[5].InnerText);
                    var grupa = DeHTML(fields[6].InnerText);
                    var sala = DeHTML(fields[8].InnerText);
                    lines.Add(new ScheduleLine(currentDate, czas_od, czas_do, subject, forma, grupa, sala));
                }
            }
        }

        string DeHTML(string text) {
            return text.Replace("\n", "").Replace("&nbsp;", "");
        }

        void UpdateCurrentDate(HtmlNode row) {
            var dateText = row.InnerText.Replace("Data Zajęć: ", "");
			var splitted = dateText.Split(' ');
			dateText = splitted[0];
			DateTime dateResult;
			if (DateTime.TryParse(dateText, out dateResult))
			{
				currentDate = dateResult;
			}
        }

        bool IsLessonRow(HtmlNode row) {
            return (row.Attributes["id"].Value.Contains("DataRow"));
        }

        bool IsDateRow(HtmlNode row) {
            return (row.Attributes["id"].Value.Contains("GroupRow"));
        }

        public void SaveToCsv() {
            using (var writer = new StreamWriter("googleCalendar.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture)) {
                csv.WriteRecords(lines);
            }
        }
    }
}