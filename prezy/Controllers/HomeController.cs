using IronBlock;
using IronBlock.Blocks;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace prezy.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Pilote()
        {
            ViewBag.Message = "Tests page for SignalR actions.";

            return View();
        }

        public ActionResult Blockly()
        {
            ViewBag.Message = "Mise en place de Blockly pour la création de scénario Prezy.";

            return View();
        }

        public ActionResult Hifi()
        {
            ViewBag.Message = "Test des commandes de l'ampli DENON AVR-X3200W.";

            return View();
        }

        public JsonResult SendCommand(string id, string display, string command, string type, string room)
        {
            //var data = Convert.FromBase64String(id);
            //var filename = Encoding.UTF8.GetString(data);
            var filename = id;
            display = display.Replace("$", "@");
            HttpClient httpClient = new HttpClient();
            JObject obj = new JObject();
            obj.Add("DisplayId", display);
            obj.Add("Command", command);
            obj.Add("Type", type);
            obj.Add("File", filename);
            obj.Add("Room", room);
            //  httpClient.PostAsync("http://demolab01/smd_presentation_core/api/actions", new StringContent(obj.ToString(), System.Text.Encoding.UTF8, "application/json"));

            PrezyHub hub = new PrezyHub();
            hub.Action(obj.ToObject<Message>());

            return Json(new { Message = "OK" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PowerOn()
        {
            Denon.PowerOn();
            return Json(new { Message = "OK" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PowerOff()
        {
            Denon.PowerOff();
            return Json(new { Message = "OK" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SelectCd()
        {
            Denon.SelectSource(CommandType.SelectCD);
            return Json(new { Message = "OK" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SelectTuner()
        {
            Denon.SelectSource(CommandType.SelectTuner);
            return Json(new { Message = "OK" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SelectServer()
        {
            Denon.SelectSource(CommandType.SelectServer);
            return Json(new { Message = "OK" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult XmlData(string xmlModel)
        {
            // Create a parser
            var parser = new Parser();

            // Add the standard blocks to the parser
            //parser.AddStandardBlocks();

            // Add custom blocs to the parser
            parser.AddBlock<Scene>("scene");
            parser.AddBlock<SceneActions>("sceneactions");
            parser.AddBlock<PowerPoint>("powerpoint");
            parser.AddBlock<Audio>("audio");
            parser.AddBlock<Video>("video");
            parser.AddBlock<Display>("display");
            parser.AddBlock<Sound>("sound");

            // parse the xml file to create a workspace
            var workspace = parser.Parse(xmlModel);

            // run the workspace
            var output = workspace.Evaluate();

            return Json(new { success = true });
        }
    }

    // LVL1-->lvl2-->lvl3-->lvl4
    public class Scene : IBlock
    {
        public override object Evaluate(Context context)
        {
            // read a field
            var myField = Fields.Get("SCENE_NAME");

            // evaluate a statement
            var myStatement = Statements.Get("SCENE_STATEMENT");
            myStatement.Evaluate(context); // evaluate your statement

            return null;
        }
    }
    // lvl1-->LVL2-->lvl3-->lvl4
    public class SceneActions : IBlock
    {
        public override object Evaluate(Context context)
        {
            // read a field
            var myField = Fields.Get("SCENEACTIONS_NAME");

            // evaluate a statement
            var myStatement = Statements.Get("SCENEACTIONS_STATEMENT");
            var ret = myStatement.Evaluate(context); // evaluate your statement eg. "VideoFile:intro.mp4@DISPLAY1|SoundFile:music.mp3@DENON|"
            // ICI on récupère l'esemble des actions à lancer dans le statement du bloc en cours
            // Thread(action1)
            // Thread(action2)
            // ...
            // Thread(actionn)
            // action1.join() action2.join() ... actionn.join()
            // Et on passe au suivant ...
            // if your block returns a value, simply `return myValue`

            // if your block is part of a statment, and another block runs after it, call
            base.Evaluate(context);
            return null;
        }
    }

    // lvl1-->lvl2-->LVL3-->lvl4
    public class PowerPoint : IBlock
    {
        public override object Evaluate(Context context)
        {
            // read a field
            var myField = Fields.Get("POWERPOINT_NAME");

            // evaluate a statement
            var myStatement = Statements.Get("POWERPOINT_STATEMENT");
            var ret = myStatement.Evaluate(context); // evaluate your statement eg. "DISPLAY1|DISPLAY2|DISPLAY3|DISPLAY4|"

            // if your block returns a value, simply `return myValue`

            // if your block is part of a statment, and another block runs after it, call
            var myBlocReturn = "PPTFile:" + myField + "@" + ret + base.Evaluate(context);
            return myBlocReturn;
        }
    }

    // lvl1-->lvl2-->LVL3-->lvl4
    public class Audio : IBlock
    {
        public override object Evaluate(Context context)
        {
            // read a field
            var myField = Fields.Get("AUDIO_NAME");

            // evaluate a statement ==> C'est le retour de tous les blocs intérieurs
            var myStatement = Statements.Get("AUDIO_STATEMENT");
            var ret = myStatement.Evaluate(context); // evaluate your statement eg. "DENON|"

            // if your block returns a value, simply `return myValue`

            // if your block is part of a statment, and another block runs after it, call
            var myBlocReturn = "SoundFile:" + myField + "@" + ret + base.Evaluate(context);
            return myBlocReturn;
        }
    }

    // lvl1-->lvl2-->LVL3-->lvl4
    public class Video : IBlock
    {
        public override object Evaluate(Context context)
        {
            // read a field
            var myField = Fields.Get("VIDEO_NAME");

            // evaluate a statement ==> C'est le retour de tous les blocs intérieurs
            var myStatement = Statements.Get("VIDEO_STATEMENT");
            var ret = myStatement.Evaluate(context); // evaluate your statement

            // if your block returns a value, simply `return myValue`

            // if your block is part of a statment, and another block runs after it, call
            //base.Evaluate(context);
            //myField = myField + "|" + base.Evaluate(context);
            var myBlocReturn = "VideoFile:" + myField + "@" + ret + base.Evaluate(context);
            return myBlocReturn;
        }
    }
    // lvl1-->lvl2-->lvl3-->LVL4
    public class Display : IBlock
    {
        public override object Evaluate(Context context)
        {
            // read a field
            var myField = Fields.Get("DISPLAY_NAME");

            // if your block returns a value, simply `return myValue`

            // if your block is part of a statment, and another block runs after it, call
            var myBlocReturn = myField + "|" +base.Evaluate(context);
            return myBlocReturn;
        }
    }
    // lvl1-->lvl2-->lvl3-->LVL4
    public class Sound : IBlock
    {
        public override object Evaluate(Context context)
        {
            // read a field
            var myField = Fields.Get("SOUND_NAME");

            // if your block returns a value, simply `return myValue`

            // if your block is part of a statment, and another block runs after it, call
            //base.Evaluate(context);
            var myBlocReturn = myField + "|" + base.Evaluate(context);
            return myBlocReturn;
        }
    }
}