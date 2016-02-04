using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulary
{
    public interface ISettings
    {
        string camera_clock { get; set; }
        string video_standard { get; set; }
        string app_status { get; set; }
        string stream_out_type { get; set; }
        string save_low_resolution_clip { get; set; }
        string video_resolution { get; set; }
        string video_stamp { get; set; }
        string video_quality { get; set; }
        string timelapse_video { get; set; }
        string photo_size { get; set; }
        string photo_stamp { get; set; }
        string photo_quality { get; set; }
        string timelapse_photo { get; set; }
        string selfie_photo { get; set; }
        string burst_photo { get; set; }
        string autoshoot_photo { get; set; }
        string loop_record { get; set; }
        string motion_detec_video { get; set; }
        string status_led_switch { get; set; }
        string wifi_led_switch { get; set; }
        string osd_switch { get; set; }
        string cardvr_switch { get; set; }
        string delay_pwroff { get; set; }
        string rotate_image { get; set; }
        string mic_vol { get; set; }
        string language { get; set; }
        string date_disp_fmt { get; set; }
        string auto_bkl_off { get; set; }
        string auto_pwr_off { get; set; }
        string light_freq { get; set; }
        string meter_mode { get; set; }
        string buzzer { get; set; }
    }
}
