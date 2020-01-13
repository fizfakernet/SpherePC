using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Graph
{
    public class RawSphere : DataFile
    {
        public string device;// прибор
        public bool must_auto_processing;// флаг необходимости автоматической обработки

        public void convert(Components comp)
        {
            this.indeficator = comp.СommentBegin;
            this.must_auto_processing = false;
            this.people = "";
            this.pipe = 0;
            this.placeXStart = comp.WidhtBegin.ToString();
            this.placeYStart = comp.LongitudetBegin.ToString();
            this.placeXFinish = comp.WidhtEnd.ToString();
            this.placeYFinish = comp.LongitudetEnd.ToString();
            this.status = "raw";
            this.type = "RawSphere";
            this.date_timeStart = comp.DatetimeBegin.ToString("yyyy-MM-dd HH:mm:ss");
            this.date_timeFinish = comp.DatetimeEnd.ToString("yyyy-MM-dd HH:mm:ss");
            this.device = "device";
            this.file_name = System.IO.Path.GetFileName(comp.file_name);
        }


        public string getJSON()
        {
            //            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(RawSphere));
            string json = JsonConvert.SerializeObject(this);
            /*
            'indeficator' => 'jnjn',//$this->indeficator, 
      'type' => $this->type,
      'date_time' => $this->date_time,
      'status' => $this->status,
      'pipe' => $this->pipe,
      'people' => $this->people,
      'placeXStart' => $this->placeXStart,
      'placeYStart' => $this->placeYStart,
      'placeXFinish' => $this->placeXFinish,
      'placeYFinish' => $this->placeYFinish,
      */

            //	return json_encode($arr);
            //            return json_encode($arr, JSON_UNESCAPED_UNICODE | JSON_UNESCAPED_SLASHES | JSON_NUMERIC_CHECK);
            return json;
        }

 /*       public void readJSON(string s)
        {
            this = JsonConvert.DeserializeObject<RawSphere>(s);
        }*/
    }
}
    