using System.Linq;
using BaseLibS.Graph;
using BaseLibS.Param;
using PerseusApi.Document;
using PerseusApi.Generic;
using PerseusApi.Matrix;

namespace PluginUpperCase
{
    public class ProcessTextColumns : IMatrixProcessing
    {
        public bool HasButton => false;
        public Bitmap2 DisplayImage => null;
        public string Description => "Change all strings to upper case.";
        public string HelpOutput => "";
        public string[] HelpSupplTables => new string[0];
        public int NumSupplTables => 0;
        public string Name => "Text to upper case";
        public string Heading => "Rearrange";
        public bool IsActive => true;
        public float DisplayRank => 22;
        public string[] HelpDocuments => new string[0];
        public int NumDocuments => 0;

        public string Url
            => "https://github.com/jdrudolph/PluginUpperCase";

        public int GetMaxThreads(Parameters parameters)
        {
            return 1;
        }

        public void ProcessData(IMatrixData mdata, Parameters param, ref IMatrixData[] supplTables,
            ref IDocumentData[] documents, ProcessInfo processInfo)
        {
            int[] inds = param.GetParam<int[]>("Columns").Value;
            bool keepColumns = param.GetParam<bool>("Keep original columns").Value;
            foreach (var col in inds)
            {
                var values = mdata.StringColumns[col].Select(s => s.ToUpper()).ToArray();
                if (keepColumns)
                {
                    mdata.AddStringColumn(mdata.StringColumnNames[col], mdata.StringColumnDescriptions[col], values);
                }
                else
                {
                    mdata.StringColumns[col] = values;
                }
            }
        }

        public Parameters GetParameters(IMatrixData mdata, ref string errorString)
        {
            return new Parameters(
                new MultiChoiceParam("Columns")
                {
                    Values = mdata.StringColumnNames
                },
                new BoolParam("Keep original columns", false)
            );
        }
    }
}