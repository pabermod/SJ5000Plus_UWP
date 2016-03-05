namespace SJ5000Plus.Models
{
    public class Settings
    {
        public Settings()
        {
            //app_status = new Param();
            auto_bkl_off = new Param();
            auto_pwr_off = new Param();
            autoshoot_photo = new Param();
            burst_photo = new Param();
            buzzer = new Param();
            //camera_clock = new Param();
            cardvr_switch = new Param();
            date_disp_fmt = new Param();
            delay_pwroff = new Param();
            language = new Param();
            light_freq = new Param();
            loop_record = new Param();
            meter_mode = new Param();
            mic_vol = new Param();
            motion_detec_video = new Param();
            osd_switch = new Param();
            photo_quality = new Param();
            photo_size = new Param();
            photo_stamp = new Param();
            rotate_image = new Param();
            save_low_resolution_clip = new Param();
            selfie_photo = new Param();
            status_led_switch = new Param();
            //stream_out_type = new Param();
            timelapse_photo = new Param();
            timelapse_video = new Param();
            video_quality = new Param();
            video_resolution = new Param();
            video_stamp = new Param();
            video_standard = new Param();
            wifi_led_switch = new Param();
        }

        public Param app_status { get; set; }
        public Param auto_bkl_off { get; set; }
        public Param auto_pwr_off { get; set; }
        public Param autoshoot_photo { get; set; }
        public Param burst_photo { get; set; }
        public Param buzzer { get; set; }
        public Param camera_clock { get; set; }
        public Param cardvr_switch { get; set; }
        public Param date_disp_fmt { get; set; }
        public Param delay_pwroff { get; set; }
        public Param language { get; set; }
        public Param light_freq { get; set; }
        public Param loop_record { get; set; }
        public Param meter_mode { get; set; }
        public Param mic_vol { get; set; }
        public Param motion_detec_video { get; set; }
        public Param osd_switch { get; set; }
        public Param photo_quality { get; set; }
        public Param photo_size { get; set; }
        public Param photo_stamp { get; set; }
        public Param rotate_image { get; set; }
        public Param save_low_resolution_clip { get; set; }
        public Param selfie_photo { get; set; }
        public Param status_led_switch { get; set; }
        public Param stream_out_type { get; set; }
        public Param timelapse_photo { get; set; }
        public Param timelapse_video { get; set; }
        public Param video_quality { get; set; }
        public Param video_resolution { get; set; }
        public Param video_stamp { get; set; }
        public Param video_standard { get; set; }
        public Param wifi_led_switch { get; set; }
    }
}