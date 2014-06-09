using System;
using System.Collections.Generic;
using System.Text;

namespace S04_ListDemo.SampleData
{
    public static class LoremIpsumGen
    {
        public static string GetLoremText(int wordcount)
        {
            StringBuilder sb = new StringBuilder();
            var words = new[]{"lorem", "ipsum", "dolor", "sit", "amet", "consectetur", "adipiscing", "elit", "Integer", "nec", "odio", 
                "praesent", "libero", "sed", "cursus", "ante", "dapibus", "diam", "sed", "nisi", "nulla", "quis", "sem", "at", "nibh", 
                "elementum", "imperdiet", "duis", "sagittis", "ipsum", "praesent", "mauris", "fusce", "nec", "tellus", "sed", "augue", 
                "semper", "porta", "mauris", "massa", "vestibulum", "lacinia", "arcu", "eget", "nulla", "class", "aptent", "taciti", 
                "sociosqu", "ad", "litora", "torquent", "per", "conubia", "nostra", "per", "inceptos", "himenaeos", "curabitur", 
                "sodales", "ligula", "in", "libero", "sed", "dignissim", "lacinia", "nunc"};

            Random r = new Random();
            for (int i = 0; i < wordcount; i++)
            {
                sb.Append(words[r.Next(0, words.Length - 1)] + " ");
            }
            sb.Remove(sb.Length - 2, 1);

            return sb.ToString();
        }

        public static string[] GetLoremTextList(int count, int wordcount)
        {
            StringBuilder sb = new StringBuilder();
            var words = new[]{"lorem", "ipsum", "dolor", "sit", "amet", "consectetur", "adipiscing", "elit", "Integer", "nec", "odio", 
                "praesent", "libero", "sed", "cursus", "ante", "dapibus", "diam", "sed", "nisi", "nulla", "quis", "sem", "at", "nibh", 
                "elementum", "imperdiet", "duis", "sagittis", "ipsum", "praesent", "mauris", "fusce", "nec", "tellus", "sed", "augue", 
                "semper", "porta", "mauris", "massa", "vestibulum", "lacinia", "arcu", "eget", "nulla", "class", "aptent", "taciti", 
                "sociosqu", "ad", "litora", "torquent", "per", "conubia", "nostra", "per", "inceptos", "himenaeos", "curabitur", 
                "sodales", "ligula", "in", "libero", "sed", "dignissim", "lacinia", "nunc"};
            var ipsumList = new string[count];
            Random r = new Random();
            for (int k = 0; k < count; k++) { 
                for (int i = 0; i < wordcount; i++)
                {
                    sb.Append(words[r.Next(0, words.Length - 1)] + " ");
                }
                sb.Remove(sb.Length - 2, 1);
                ipsumList[k] = sb.ToString();
                sb.Clear();
            }
            return ipsumList;
        }


        public static string[] GetCompanyList(int companyCount)
        {
            var companies = new[]{"A. Datum Corporation", "Adventure Works", "Alpine Ski House", "Blue Yonder Airlines",  "City Power &amp; Light",
                                   "Coho Vineyard", "Coho Winery", "Coho Vineyard &amp; Winery", "Contoso, Ltd", "Contoso Pharmaceuticals", "Consolidated Messenge",  
                                    "Fabrikam, Inc.", "Fourth Coffee", "Graphic Design", "Institute Humongous", "Insurance Litware, Inc.", "Lucerne Publishing", "Margie's Travel", 
                                    "Northwind Traders", "Proseware, Inc.", "School of Fine Art", "Southridge Video", "Tailspin Toys", "The Phone Company", "Wide World Importers", 
                                    "Wingtip Toys", "Woodgrove Bank"};
            Random r = new Random();
            string[] returnCompanies = new string[companyCount];
            for(int i = 0; i < companyCount; i++)
            {
                returnCompanies[i] = companies[r.Next(0, companies.Length - 1)];
            }

            return returnCompanies;
        }

        public static string[] GetImagesList(int count)
        {
            var images = new[]{"ms-appx:/SampleData/Images/image01.png", 
                                "ms-appx:/SampleData/Images/image02.png", 
                                "ms-appx:/SampleData/Images/image03.png",
                                "ms-appx:/SampleData/Images/image04.png", 
                                "ms-appx:/SampleData/Images/image05.png"};
            string[] returnImages = new string[count];
            int imagePos = 0;
            for (int i = 0; i < count; i++)
            {
                returnImages[i] = images[imagePos];
                imagePos++;
                if (imagePos== 5)
                    imagePos = 0;
            }

            return returnImages;
        }


    }
}
