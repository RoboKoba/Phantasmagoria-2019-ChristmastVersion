using UnityEngine;
using Pcx;

[ExecuteInEditMode]
public class PointAnimation : MonoBehaviour
{
    [SerializeField] PointCloudData _sourceData;
    [SerializeField] ComputeShader _computeShader;

    public float _param1;
    public float _param2;
    public float _param3;
    public float _param4;

    ComputeBuffer _pointBuffer;

    void OnDisable()
    {
        if (_pointBuffer != null)
        {
            _pointBuffer.Release();
            _pointBuffer = null;
        }
    }

    void Update()
    {
        if (_sourceData == null) return;

        var sourceBuffer = _sourceData.computeBuffer;

        if (_pointBuffer == null || _pointBuffer.count != sourceBuffer.count)
        {
            if (_pointBuffer != null) _pointBuffer.Release();
            _pointBuffer = new ComputeBuffer(sourceBuffer.count, PointCloudData.elementSize);
        }

        var time = Application.isPlaying ? Time.time : 0;

        var kernel = _computeShader.FindKernel("Main");
        _computeShader.SetFloat("Param1", _param1);
        _computeShader.SetFloat("Param2", _param2);
        _computeShader.SetFloat("Param3", _param3);
        _computeShader.SetFloat("Param4", _param4);
        _computeShader.SetFloat("Time", time);
        _computeShader.SetBuffer(kernel, "SourceBuffer", sourceBuffer);
        _computeShader.SetBuffer(kernel, "OutputBuffer", _pointBuffer);
        _computeShader.Dispatch(kernel, sourceBuffer.count / 128, 1, 1);

        GetComponent<PointCloudRenderer>().sourceBuffer = _pointBuffer;
    }
}
