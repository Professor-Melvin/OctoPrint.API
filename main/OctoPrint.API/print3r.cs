///* 
//// OctoPrint API wrapper
//// csc /target:library Octo.cs /r:System.Net.Http.dll
//// linqpad
//var server = "http://octopi.local";
////	print3r.Reader<print3r.Logs.Result>.Read(server).Dump();
////	print3r.Reader<print3r.Connection.Result>.Read(server).Dump();
////	print3r.Reader<print3r.Files.Result>.Read(server).Dump();
////	print3r.Reader<print3r.Job.Result>.Read(server).Dump();
////	print3r.Reader<print3r.Printer.Result>.Read(server).Dump();
////	print3r.Reader<print3r.Settings.Result>.Read(server).Dump();
////	print3r.Reader<print3r.TimeLapse.Result>.Read(server).Dump();
////	print3r.Reader<print3r.Users.Result>.Read(server).Dump();
////	print3r.Reader<print3r.State.Result>.Read(server).Dump();
////	print3r.Reader<print3r.Version.Result>.Read(server).Dump();
//(new print3r.Everything(server)).Dump();
// */

//using System;
//using System.IO;
//using System.Linq;
//using System.Net;

//namespace print3r
//{
//    public class ApiAttribute : Attribute
//    {
//        public string Path { get; private set; }
//        public ApiAttribute(string path)
//        {
//            Path = path;
//        }
//    }
//    public class Reader<T> where T : ResultBase, new()
//    {
//        public static T Read(string server)
//        {
//            var api = typeof(T).GetCustomAttributes(typeof(ApiAttribute), false).FirstOrDefault() as ApiAttribute;
//            if (api == null) throw new InvalidOperationException("Result type must have an ApiAttribute");
//            var ser = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
//            try
//            {
//                var req = WebRequest.Create(server + api.Path);
//                using (var res = req.GetResponse())
//                {
//                    return (T)ser.ReadObject(res.GetResponseStream());
//                }
//            }
//            catch (WebException ex)
//            {
//                // Only "normally" used for /api/printer when no printer is attached.
//                return new T() { Error = (new StreamReader(ex.Response.GetResponseStream())).ReadToEnd() };
//            }
//        }
//    }
//    public class Everything
//    {
//        public Everything(string server)
//        {
//            Connection = Reader<Connection.Result>.Read(server);
//            Files = Reader<Files.Result>.Read(server);
//            Job = Reader<Job.Result>.Read(server);
//            Logs = Reader<Logs.Result>.Read(server);
//            Printer = Reader<Printer.Result>.Read(server);
//            Settings = Reader<Settings.Result>.Read(server);
//            State = Reader<State.Result>.Read(server);
//            TimeLapse = Reader<TimeLapse.Result>.Read(server);
//            Users = Reader<Users.Result>.Read(server);
//            Version = Reader<Version.Result>.Read(server);
//        }
//        public Connection.Result Connection { get; private set; }
//        public Files.Result Files { get; private set; }
//        public Job.Result Job { get; private set; }
//        public Logs.Result Logs { get; private set; }
//        public Printer.Result Printer { get; private set; }
//        public Settings.Result Settings { get; private set; }
//        public State.Result State { get; private set; }
//        public TimeLapse.Result TimeLapse { get; private set; }
//        public Users.Result Users { get; private set; }
//        public Version.Result Version { get; private set; }
//    }
//    public class ResultBase
//    {
//        public string Error { get; set; }
//    }
//    namespace Logs
//    {
//        [Api("/api/logs")]
//        public class Result : ResultBase
//        {
//            public File[] files { get; set; }
//            public long free { get; set; }
//        }
//        public class File
//        {
//            public int? date { get; set; }
//            public Refs refs { get; set; }
//            public string name { get; set; }
//            public int? size { get; set; }
//        }
//        public class Refs
//        {
//            public string download { get; set; }
//            public string resource { get; set; }
//        }
//    }
//    namespace Connection
//    {
//        [Api("/api/connection")]
//        public class Result : ResultBase
//        {
//            public Current current { get; set; }
//            public Options options { get; set; }
//        }
//        public class Current
//        {
//            public int? baudrate { get; set; }
//            public string state { get; set; }
//            public string port { get; set; }
//        }
//        public class Options
//        {
//            public string portPreference { get; set; }
//            public bool? autoconnect { get; set; }
//            public int[] baudrates { get; set; }
//            public string[] ports { get; set; }
//            public string baudratePreference { get; set; }
//        }
//    }
//    namespace Files
//    {
//        [Api("/api/files")]
//        public class Result : ResultBase
//        {
//            public File[] files { get; set; }
//            public long free { get; set; }
//        }
//        public class File
//        {
//            public string origin { get; set; }
//            public Prints prints { get; set; }
//            public string name { get; set; }
//            public Refs refs { get; set; }
//            public Gcodeanalysis gcodeAnalysis { get; set; }
//            public int? date { get; set; }
//            public int? size { get; set; }
//        }
//        public class Prints
//        {
//            public int? failure { get; set; }
//            public Last last { get; set; }
//            public int? success { get; set; }
//        }
//        public class Last
//        {
//            public float? date { get; set; }
//            public bool? success { get; set; }
//        }
//        public class Refs
//        {
//            public string download { get; set; }
//            public string resource { get; set; }
//        }
//        public class Gcodeanalysis
//        {
//            public float? estimatedPrintTime { get; set; }
//            public Filament filament { get; set; }
//        }
//        public class Filament
//        {
//            public Tool0 tool0 { get; set; }
//        }
//        public class Tool0
//        {
//            public float? volume { get; set; }
//            public float? length { get; set; }
//        }
//        //public class Actions
//        //{
//        //    public static void AddFile(string server, string name, byte[] bytes)
//        //    {
//        //        using (var client = new HttpClient() { BaseAddress = new Uri(server) })
//        //        {
//        //            client.PostAsJsonAsync("/api/file/" + name, new { files = new { file = bytes } })
//        //            .ContinueWith((t) =>
//        //            {
//        //                var bar = t.Result;
//        //            });
//        //        }
//        //    }
//        //}
//    }
//    namespace Job
//    {
//        [Api("/api/job")]
//        public class Result : ResultBase
//        {
//            public Progress progress { get; set; }
//            public Job job { get; set; }
//            public string state { get; set; }
//        }
//        public class Progress
//        {
//            public float? completion { get; set; }
//            public int? printTime { get; set; }
//            public int? filepos { get; set; }
//            public int? printTimeLeft { get; set; }
//        }
//        public class Job
//        {
//            public float? estimatedPrintTime { get; set; }
//            public Filament filament { get; set; }
//            public File file { get; set; }
//        }
//        public class Filament
//        {
//            public Tool0 tool0 { get; set; }
//        }
//        public class Tool0
//        {
//            public float? volume { get; set; }
//            public float? length { get; set; }
//        }
//        public class File
//        {
//            public string origin { get; set; }
//            public int? date { get; set; }
//            public string name { get; set; }
//            public int? size { get; set; }
//        }
//    }
//    namespace Printer
//    {
//        [Api("/api/printer")]
//        public class Result : ResultBase
//        {
//            public Temps temps { get; set; }
//        }
//        public class Temps
//        {
//            public Bed bed { get; set; }
//            public Tool2 tool2 { get; set; }
//            public Tool1 tool1 { get; set; }
//            public Tool0 tool0 { get; set; }
//        }
//        public class Bed
//        {
//            public float? actual { get; set; }
//            public float? target { get; set; }
//            public int? offset { get; set; }
//        }
//        public class Tool2
//        {
//            public float? actual { get; set; }
//            public float? target { get; set; }
//            public int? offset { get; set; }
//        }
//        public class Tool1
//        {
//            public float? actual { get; set; }
//            public float? target { get; set; }
//            public int? offset { get; set; }
//        }
//        public class Tool0
//        {
//            public float? actual { get; set; }
//            public float? target { get; set; }
//            public int? offset { get; set; }
//        }
//        //public class Command
//        //{
//        //    public object[] controls { get; set; }
//        //}
//    }
//    namespace Settings
//    {
//        [Api("/api/settings")]
//        public class Result : ResultBase
//        {
//            public Webcam webcam { get; set; }
//            public Printer printer { get; set; }
//            public Temperature temperature { get; set; }
//            public Appearance appearance { get; set; }
//            public Feature feature { get; set; }
//            public System system { get; set; }
//            public Api api { get; set; }
//            public Terminalfilter[] terminalFilters { get; set; }
//            public Cura cura { get; set; }
//            public Serial serial { get; set; }
//            public Folder folder { get; set; }
//        }
//        public class Webcam
//        {
//            public string streamUrl { get; set; }
//            public bool? watermark { get; set; }
//            public bool? flipV { get; set; }
//            public string bitrate { get; set; }
//            public string snapshotUrl { get; set; }
//            public bool? flipH { get; set; }
//            public string ffmpegPath { get; set; }
//        }
//        public class Printer
//        {
//            public int? movementSpeedZ { get; set; }
//            public Extruderoffset[] extruderOffsets { get; set; }
//            public int? movementSpeedX { get; set; }
//            public int? movementSpeedY { get; set; }
//            public object[] invertAxes { get; set; }
//            public int? numExtruders { get; set; }
//            public int? movementSpeedE { get; set; }
//            public Beddimensions bedDimensions { get; set; }
//        }
//        public class Beddimensions
//        {
//            public float? y { get; set; }
//            public float? x { get; set; }
//        }
//        public class Extruderoffset
//        {
//            public float? y { get; set; }
//            public float? x { get; set; }
//        }
//        public class Temperature
//        {
//            public Profile[] profiles { get; set; }
//        }
//        public class Profile
//        {
//            public string name { get; set; }
//            public int? bed { get; set; }
//            public int? extruder { get; set; }
//        }
//        public class Appearance
//        {
//            public string color { get; set; }
//            public string name { get; set; }
//        }
//        public class Feature
//        {
//            public bool? gcodeViewer { get; set; }
//            public bool? swallowOkAfterResend { get; set; }
//            public bool? waitForStart { get; set; }
//            public bool? sdAlwaysAvailable { get; set; }
//            public bool? temperatureGraph { get; set; }
//            public bool? repetierTargetTemp { get; set; }
//            public bool? alwaysSendChecksum { get; set; }
//            public bool? sdSupport { get; set; }
//        }
//        public class System
//        {
//            public object events { get; set; }
//            public Action[] actions { get; set; }
//        }
//        public class Action
//        {
//            public string action { get; set; }
//            public string command { get; set; }
//            public string name { get; set; }
//            public string confirm { get; set; }
//        }
//        public class Api
//        {
//            public bool? enabled { get; set; }
//            public string key { get; set; }
//        }
//        public class Cura
//        {
//            public string path { get; set; }
//            public string config { get; set; }
//            public bool? enabled { get; set; }
//        }
//        public class Serial
//        {
//            public float? timeoutConnection { get; set; }
//            public object baudrate { get; set; }
//            public float? timeoutSdStatus { get; set; }
//            public string[] portOptions { get; set; }
//            public string port { get; set; }
//            public bool? autoconnect { get; set; }
//            public bool? log { get; set; }
//            public float? timeoutDetection { get; set; }
//            public float? timeoutTemperature { get; set; }
//            public float? timeoutCommunication { get; set; }
//            public int[] baudrateOptions { get; set; }
//        }
//        public class Folder
//        {
//            public string logs { get; set; }
//            public string timelapse { get; set; }
//            public string uploads { get; set; }
//            public string timelapseTmp { get; set; }
//        }
//        public class Terminalfilter
//        {
//            public string regex { get; set; }
//            public string name { get; set; }
//        }
//    }
//    namespace TimeLapse
//    {
//        [Api("/api/timelapse")]
//        public class Result : ResultBase
//        {
//            public object[] files { get; set; }
//            public Config config { get; set; }
//        }
//        public class Config
//        {
//            public string type { get; set; }
//        }
//    }
//    namespace Users
//    {
//        [Api("/api/users")]
//        public class Result : ResultBase
//        {
//            public object[] files { get; set; }
//            public Config config { get; set; }
//        }
//        public class Config
//        {
//            public string type { get; set; }
//        }
//    }
//    namespace State
//    {
//        [Api("/api/state")]
//        public class Result : ResultBase
//        {
//            public State state { get; set; }
//            public Temperatures temperatures { get; set; }
//            public Offsets offsets { get; set; }
//            public Progress progress { get; set; }
//            public float? currentZ { get; set; }
//            public Job job { get; set; }
//        }
//        public class State
//        {
//            public int? state { get; set; }
//            public string stateString { get; set; }
//            public Flags flags { get; set; }
//        }
//        public class Flags
//        {
//            public bool? operational { get; set; }
//            public bool? paused { get; set; }
//            public bool? printing { get; set; }
//            public bool? sdReady { get; set; }
//            public bool? error { get; set; }
//            public bool? ready { get; set; }
//            public bool? closedOrError { get; set; }
//        }
//        public class Temperatures
//        {
//            public Bed bed { get; set; }
//            public Tool2 tool2 { get; set; }
//            public Tool1 tool1 { get; set; }
//            public Tool0 tool0 { get; set; }
//        }
//        public class Bed
//        {
//            public float? actual { get; set; }
//            public float? target { get; set; }
//            public int? offset { get; set; }
//        }
//        public class Tool2
//        {
//            public float? actual { get; set; }
//            public float? target { get; set; }
//            public int? offset { get; set; }
//        }
//        public class Tool1
//        {
//            public float? actual { get; set; }
//            public float? target { get; set; }
//            public int? offset { get; set; }
//        }
//        public class Tool0
//        {
//            public float? actual { get; set; }
//            public float? target { get; set; }
//            public int? offset { get; set; }
//        }
//        public class Offsets
//        {
//        }
//        public class Progress
//        {
//            public float? completion { get; set; }
//            public int? printTime { get; set; }
//            public int? filepos { get; set; }
//            public int? printTimeLeft { get; set; }
//        }
//        public class Job
//        {
//            public float? estimatedPrintTime { get; set; }
//            public Filament filament { get; set; }
//            public File file { get; set; }
//        }
//        public class Filament
//        {
//            public Tool01 tool0 { get; set; }
//        }
//        public class Tool01
//        {
//            public float? volume { get; set; }
//            public float? length { get; set; }
//        }
//        public class File
//        {
//            public string origin { get; set; }
//            public int? date { get; set; }
//            public string name { get; set; }
//            public int? size { get; set; }
//        }
//    }
//    namespace Version
//    {
//        [Api("/api/version")]
//        public class Result : ResultBase
//        {
//            public string api { get; set; }
//            public string server { get; set; }
//        }
//    }
//}