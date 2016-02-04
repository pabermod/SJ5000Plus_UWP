using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace SJ5000Plus.Models
{
    public class Settings : BindableBase, Vocabulary.ISettings
    {
        private string _camera_clock; // yyyy-mm-dd HH:MM:SS //No añadido
        private string _video_standard;
        private string _app_status;  // idle, vf, record, capture. Not settable
        private string _stream_out_type;  // empty. Set to "rtsp"
        private string _save_low_resolution_clip;  // On-Off // No añadido
        private string _video_resolution;
        private string _video_stamp;
        private string _video_quality;
        private string _timelapse_video;
        private string _photo_size;
        private string _photo_stamp;
        private string _photo_quality;
        private string _timelapse_photo;  //It has no values ?? // No añadido
        private string _selfie_photo;
        private string _burst_photo;
        private string _autoshoot_photo;
        private string _loop_record;
        private string _motion_detec_video;
        private string _status_led_switch; 
        private string _wifi_led_switch;  // On-Off // No añadido
        private string _osd_switch;  // On-Off // No añadido
        private string _cardvr_switch;  // On-Off // No añadido
        private string _delay_pwroff;
        private string _rotate_image;
        private string _mic_vol;
        private string _language;
        private string _date_disp_fmt;
        private string _auto_bkl_off;
        private string _auto_pwr_off;
        private string _light_freq;
        private string _meter_mode;
        private string _buzzer;  // On-Off // No añadido

        public string camera_clock { get { return _camera_clock; } set { Set(ref _camera_clock, value); } }
        public string video_standard { get { return _video_standard; } set { Set(ref _video_standard, value); } }
        public string app_status { get { return _app_status; } set { Set(ref _app_status, value); } }
        public string stream_out_type { get { return _stream_out_type; } set { Set(ref _stream_out_type, value); } }
        public string save_low_resolution_clip { get { return _save_low_resolution_clip; } set { Set(ref _save_low_resolution_clip, value); } }
        public string video_resolution { get { return _video_resolution; } set { Set(ref _video_resolution, value); } }
        public string video_stamp { get { return _video_stamp; } set { Set(ref _video_stamp, value); } }
        public string video_quality { get { return _video_quality; } set { Set(ref _video_quality, value); } }
        public string timelapse_video { get { return _timelapse_video; } set { Set(ref _timelapse_video, value); } }
        public string photo_size { get { return _photo_size; } set { Set(ref _photo_size, value); } }
        public string photo_stamp { get { return _photo_stamp; } set { Set(ref _photo_stamp, value); } }
        public string photo_quality { get { return _photo_quality; } set { Set(ref _photo_quality, value); } }
        public string timelapse_photo { get { return _timelapse_photo; } set { Set(ref _timelapse_photo, value); } }
        public string selfie_photo { get { return _selfie_photo; } set { Set(ref _selfie_photo, value); } }
        public string burst_photo { get { return _burst_photo; } set { Set(ref _burst_photo, value); } }
        public string autoshoot_photo { get { return _autoshoot_photo; } set { Set(ref _autoshoot_photo, value); } }
        public string loop_record { get { return _loop_record; } set { Set(ref _loop_record, value); } }
        public string motion_detec_video { get { return _motion_detec_video; } set { Set(ref _motion_detec_video, value); } }

        public string status_led_switch { get { return _status_led_switch; } set { Set(ref _status_led_switch, value); } }
        public string wifi_led_switch { get { return _wifi_led_switch; } set { Set(ref _wifi_led_switch, value); } }
        public string osd_switch { get { return _osd_switch; } set { Set(ref _osd_switch, value); } }
        public string cardvr_switch { get { return _cardvr_switch; } set { Set(ref _cardvr_switch, value); } }
        public string delay_pwroff { get { return _delay_pwroff; } set { Set(ref _delay_pwroff, value); } }
        public string rotate_image { get { return _rotate_image; } set { Set(ref _rotate_image, value); } }
        public string mic_vol { get { return _mic_vol; } set { Set(ref _mic_vol, value); } }
        public string language { get { return _language; } set { Set(ref _language, value); } }
        public string date_disp_fmt { get { return _date_disp_fmt; } set { Set(ref _date_disp_fmt, value); } }
        public string auto_bkl_off { get { return _auto_bkl_off; } set { Set(ref _auto_bkl_off, value); } }
        public string auto_pwr_off { get { return _auto_pwr_off; } set { Set(ref _auto_pwr_off, value); } }
        public string light_freq { get { return _light_freq; } set { Set(ref _light_freq, value); } }
        public string meter_mode { get { return _meter_mode; } set { Set(ref _meter_mode, value); } }
        public string buzzer { get { return _buzzer; } set { Set(ref _buzzer, value); } }
    }
}
