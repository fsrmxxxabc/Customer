using System.IO;
using System.Windows.Documents;
using System.Windows.Documents.Serialization;

namespace Customer.Until.Chat
{
    class Serializes
    {

        public static void instance(FlowDocument flowDocument)
        {
            string fileName = "../../Resources/Content/Content.html";
            // Create a SerializerProvider for accessing plug-in serializers.
            System.Windows.Documents.Serialization.SerializerProvider serializerProvider = new System.Windows.Documents.Serialization.SerializerProvider();


            // Locate the serializer that matches the fileName extension.
            /*SerializerDescriptor selectedPlugIn = null;
            foreach (SerializerDescriptor serializerDescriptor in
                            serializerProvider.InstalledSerializers)
            {
                if (serializerDescriptor.IsLoadable &&
                     fileName.EndsWith(serializerDescriptor.DefaultFileExtension))
                {   // The plug-in serializer and fileName extensions match.
                    selectedPlugIn = serializerDescriptor;
                    break; // foreach
                }
            }

            // If a match for a plug-in serializer was found,
            // use it to output and store the document.
            if (selectedPlugIn != null)
            {
                Stream package = File.Create(fileName);
                SerializerWriter serializerWriter =
                    serializerProvider.CreateSerializerWriter(selectedPlugIn,
                                                              package);
                IDocumentPaginatorSource idoc =
                    flowDocument as IDocumentPaginatorSource;
                serializerWriter.Write(idoc.DocumentPaginator, null);
                package.Close();
                //return true;
            }*/
        }

        // ------------------------ PlugInFileFilter --------------------------
        /// <summary>
        ///   Gets a filter string for installed plug-in serializers.</summary>
        /// <remark>
        ///   PlugInFileFilter is used to set the SaveFileDialog or
        ///   OpenFileDialog "Filter" property when saving or opening files
        ///   using plug-in serializers.</remark>
        private string PlugInFileFilter
        {
            get
            {   // Create a SerializerProvider for accessing plug-in serializers.
                SerializerProvider serializerProvider = new SerializerProvider();
                string filter = "";

                // For each loadable serializer, add its display
                // name and extension to the filter string.
                foreach (SerializerDescriptor serializerDescriptor in
                    serializerProvider.InstalledSerializers)
                {
                    if (serializerDescriptor.IsLoadable)
                    {
                        // After the first, separate entries with a "|".
                        if (filter.Length > 0) filter += "|";

                        // Add an entry with the plug-in name and extension.
                        filter += serializerDescriptor.DisplayName + " (*" +
                            serializerDescriptor.DefaultFileExtension + ")|*" +
                            serializerDescriptor.DefaultFileExtension;
                    }
                }

                // Return the filter string of installed plug-in serializers.
                return filter;
            }
        }
    }
}
