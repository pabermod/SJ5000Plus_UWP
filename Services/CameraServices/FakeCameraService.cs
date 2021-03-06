﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SJ5000Plus.Services.CameraServices
{
    public class FakeCameraService : ICameraService
    {
        private bool _connected;
        private bool _recording;

        public FakeCameraService()
        {
            _connected = false;
            _recording = false;
        }

        public bool isConnected
        {
            get { return _connected; }
            set { _connected = value; }
        }

        public bool isRecording
        {
            get { return _recording; }
            set { _recording = value; }
        }

        public int token { get; set; }

        public async Task<bool> Connect()
        {
            _connected = true;
            return _connected;
        }

        public async Task<bool> Disconnect()
        {
            _connected = false;
            return !_connected;
        }

        public async Task<Vocabulary.Settings> GetCurrentValues()
        {
            Vocabulary.Settings sett = new Vocabulary.Settings();
            sett.video_resolution = "1080p";
            sett.video_standard = "NTSC";
            sett.video_quality = "Super Fine";
            sett.video_stamp = "Date/Time";
            sett.timelapse_video = "10";
            sett.loop_record = "5";
            sett.motion_detec_video = "High";

            sett.photo_size = "16 MP";
            sett.photo_quality = "Fine";
            sett.photo_stamp = "Date";
            sett.selfie_photo = "5";
            sett.burst_photo = "10";
            sett.autoshoot_photo = "3";
            return sett;
        }

        public async Task<Vocabulary.Messages.CamGetParamValuesMessage> GetParamValues(string param)
        {
            Vocabulary.Messages.CamGetParamValuesMessage Param = new Vocabulary.Messages.CamGetParamValuesMessage(0, 0, param, "settable", new List<string>());

            if (param.Equals("video_resolution"))
            {
                Param.options.Add("1080p");
                Param.options.Add("720p");
                Param.options.Add("480p");
            }
            else if (param.Equals("video_standard"))
            {
                Param.options.Add("NTSC");
                Param.options.Add("PAL");
            }
            else if (param.Equals("photo_size"))
            {
                Param.options.Add("20 MP");
                Param.options.Add("16 MP");
                Param.options.Add("12 MP");
            }
            else if (param.Equals("video_quality"))
            {
                Param.options.Add("Super Fine");
                Param.options.Add("Fine");
                Param.options.Add("Normal");
            }
            return Param;
        }

        public async Task<bool> GetToken()
        {
            token = new Random().Next(1, 20);
            return true;
        }

        public async Task<bool> SetParamValue(string Param, string Value, string Permission)
        {
            Vocabulary.Messages.CamParamMessage Msg = new Vocabulary.Messages.CamParamMessage(9, 0, Value);
            return true;
        }

        public async Task<bool> StartVideo()
        {
            _recording = true;
            return true;
        }

        public async Task<string> StopVideo()
        {
            _recording = false;
            return "DCIM/100MEDIA/SJCM0004.mp4";
        }

        public async Task<string> TakePhoto()
        {
            return "DCIM/100MEDIA/SJCM0006.jpg";
        }
    }
}