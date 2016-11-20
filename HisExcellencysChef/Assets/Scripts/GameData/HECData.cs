/*
 * Author(s): Isaiah Mann
 * Description: Generic class for data in Chef
 */

[System.Serializable]
public class HECData {

}

[System.Serializable]
public class HECDataList<T> where T : HECData {
	public T[] Elements;
	public int Length {
		get {
			return Elements.Length;
		}
	}

	public override string ToString () {
		return string.Format (ArrayUtil.ToString(Elements));
	}
}