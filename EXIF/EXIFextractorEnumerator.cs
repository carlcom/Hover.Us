using System.Collections;

namespace VTSV.EXIF
{
    sealed class EXIFextractorEnumerator : IEnumerator
    {
        IDictionaryEnumerator index;

        public EXIFextractorEnumerator(IDictionary exif)
        {
            Reset();
            index = exif.GetEnumerator();
        }

        #region IEnumerator Members

        public void Reset()
        {
            index = null;
        }

        public object Current
        {
            get
            {
                return (new System.Web.UI.Pair(index.Key, index.Value));
            }
        }

        public bool MoveNext()
        {
            return index != null && index.MoveNext();
        }

        #endregion

    }
}