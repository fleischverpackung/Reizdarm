#region usings
using System;
using System.ComponentModel.Composition;
using System.Collections.Generic;

using VVVV.PluginInterfaces.V1;
using VVVV.PluginInterfaces.V2;
using VVVV.Utils.VColor;
using VVVV.Utils.VMath;

using VVVV.Core.Logging;
#endregion usings

namespace VVVV.Nodes
{
	#region PluginInfo
	[PluginInfo(Name = "Gundel", Category = "Value", Help = "Gundel is only interested in very special Data Sets", Tags = "")]
	#endregion PluginInfo


	public class ValueGundelNode : IPluginEvaluate
	{
		public class PersistItem
		{
			public List<int> key;
			public string name;
			public RGBAColor color;
			public List<double> values;
		}

		#region fields & pins
		[Input("Index", DefaultValue = 1.0)]
		IDiffSpread<int> FIDInput;

		[Input("Index Vector Size", DefaultValue = 1.0)]
		IDiffSpread<int> FIndexVectorSize;


		[Input("Value", DefaultValue = 0.0)]
		ISpread<double> FValueIn;

		[Input("Value Vector Size", DefaultValue = 1.0)]
		IDiffSpread<int> FValueVectorSize;

		[Input("String", DefaultString = "")]
		ISpread<string> FStringIn;

		[Input("Color", DefaultColor = new double[] {
			0.1,
			0.2,
			0.3,
			1.0
		})]
		ISpread<RGBAColor> FColorIn;

		[Input("Reset", DefaultValue = 0.0)]
		ISpread<bool> FReset;

		[Input("GetIndex", DefaultValue = 1.0)]
		IDiffSpread<int> FGetID;

		[Output("Index")]
		ISpread<int> FIDOutput;

		[Output("Value", DefaultValue = 0.0)]
		ISpread<double> FValueOut;

		[Output("String")]
		ISpread<string> FStringOut;

		[Output("Output")]
		ISpread<RGBAColor> FColorOut;

		[Import()]
		ILogger FLogger;
		SortedDictionary<string, PersistItem> dict;

		#endregion fields & pins

		//called when data for any output pin is requested
		public void Evaluate(int SpreadMax)
		{

			if (FReset[0] || FIndexVectorSize.IsChanged || FValueVectorSize.IsChanged || dict == null) {
				dict = new SortedDictionary<string, PersistItem>();
			}

			SpreadMax = Math.Max((int)Math.Ceiling(FIDInput.SliceCount / (double)FIndexVectorSize[0]), FColorIn.SliceCount);
			SpreadMax = Math.Max((int)Math.Ceiling(FValueIn.SliceCount / (double)FValueVectorSize[0]), SpreadMax);
			SpreadMax = Math.Max(FStringIn.SliceCount, SpreadMax);

			for (int i = 0; i < SpreadMax; i++) {
				string key = "";
				char pad = '-';
				int id;
				for (int j = 0; j < FIndexVectorSize[0]; j++) {
					id = FIDInput[i * FIndexVectorSize[0] + j];
					if (id < 0) {
						id = int.MaxValue - id;
						pad = '-';
					} else
						pad = '0';
					string snippet = id.ToString();
					snippet = pad + snippet.PadLeft(int.MaxValue.ToString().Length, '0');
					key += snippet + " ";
				}

				if (!dict.ContainsKey(key))
					dict[key] = new PersistItem();
				if (dict[key].key == null)
					dict[key].key = new List<int>();
				else
					dict[key].key.Clear();
				for (int j = 0; j < FIndexVectorSize[0]; j++) {
					dict[key].key.Add(FIDInput[i * FIndexVectorSize[0] + j]);

				}
				dict[key].name = FStringIn[i];
				dict[key].color = FColorIn[i];


				if (dict[key].values == null)
					dict[key].values = new List<double>();
				else
					dict[key].values.Clear();
				for (int j = 0; j < FValueVectorSize[0]; j++) {
					dict[key].values.Add(FValueIn[i * FValueVectorSize[0] + j]);
				}
			}

			SpreadMax = (int)Math.Ceiling(FGetID.SliceCount/(double)FIndexVectorSize[0]);

			FIDOutput.SliceCount = 0;
			FValueOut.SliceCount = 0;
			FStringOut.SliceCount = 0;
			FColorOut.SliceCount = 0;

//			FIDOutput.SliceCount = SpreadMax * FIndexVectorSize[0];
//			FValueOut.SliceCount = SpreadMax * FValueVectorSize[0];
//			FStringOut.SliceCount = SpreadMax;

			for (int i = 0; i < SpreadMax; i++) {
				string key = "";
				char pad = '-';
				int id;
				for (int j = 0; j < FIndexVectorSize[0]; j++) {
					id = FGetID[i * FIndexVectorSize[0] + j];
					if (id < 0) {
						id = int.MaxValue - id;
						pad = '-';
					} else
						pad = '0';
					string snippet = id.ToString();
					snippet = pad + snippet.PadLeft(int.MaxValue.ToString().Length, '0');
					key += snippet + " ";
				}
				
				if (dict.ContainsKey(key)) {
					PersistItem item = dict[key];
					for (int j = 0;j<FIndexVectorSize[0];j++) FIDOutput.Add(item.key[j]);
					for (int j = 0;j<FValueVectorSize[0];j++) FValueOut.Add(item.values[j]);
					FColorOut.Add(item.color);
					FStringOut.Add(item.name);
					
					
				}
				
			}


		}
	}
}
