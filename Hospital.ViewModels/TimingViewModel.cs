using Hospital.Models.Enums;
using Hospital.Models;
using System.Web.Mvc;

namespace Hospital.ViewModels
{
    public class TimingViewModel
    {
        public int Id                      { get; set; }
        public ApplicationUser DoctorId    { get; set; }
        public DateTime Date               { get; set; }
        public int MorningShiftStartTime   { get; set; }
        public int MorningShiftEndTime     { get; set; }
        public int AfternoonShiftStartTime { get; set; }
        public int AfternoonShiftEndTime   { get; set; }
        public int Duration                { get; set; }
        public Status Status               { get; set; }

        List<SelectListItem> morningShiftStart   = new List<SelectListItem>();
        List<SelectListItem> morningShiftEnd     = new List<SelectListItem>();
        List<SelectListItem> afternoonShiftStart = new List<SelectListItem>();
        List<SelectListItem> afternoonShiftEnd   = new List<SelectListItem>();

        public TimingViewModel()
        {
        }

        public TimingViewModel(Timing model)
        {
            Id                      = model.Id;
            Date                    = model.Date;
            MorningShiftStartTime   = model.MorningShiftStartTime;
            MorningShiftEndTime     = model.MorningShiftEndTime;
            AfternoonShiftEndTime   = model.MorningShiftEndTime;
            AfternoonShiftStartTime = model.MorningShiftStartTime;
            Duration                = model.Duration;
            Status                  = model.Status;
        }

        public Timing ConvertViewModel(TimingViewModel model)
        {
            return new Timing
            {
                Id                      = model.Id,
                Date                    = model.Date,
                MorningShiftStartTime   = model.MorningShiftStartTime,
                MorningShiftEndTime     = model.MorningShiftEndTime,
                AfternoonShiftEndTime   = model.AfternoonShiftEndTime,
                AfternoonShiftStartTime = model.AfternoonShiftStartTime,
                Duration                = model.Duration,
                Status                  = model.Status
            };
        }
    }
}
