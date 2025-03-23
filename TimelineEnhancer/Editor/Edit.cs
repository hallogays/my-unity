using UnityEditor;
using UnityEngine;
[MenuItem("Tools/Timeline Enhancer/Quick Keyframe Edit")]
static void ShowQuickKeyframeEditor() {
    QuickKeyframeWindow.GetWindow<QuickKeyframeWindow>("Quick Keyframe");
}

// 快捷键注册
[InitializeOnLoadMethod]
static void Initialize() {
    KeyboardEvent.RegisterShortcut("Ctrl+D", () => CopyKeyframes());
    KeyboardEvent.RegisterShortcut("Ctrl+V", () => PasteKeyframes());
}
static class KeyframeToolkit {
    static void CopyKeyframes(AnimationClip clip) {
        // 实现关键帧复制逻辑
        Keyframe[] keys = clipgetKeyframes();
        // 存储到剪贴板
        EditorUtility.SetObjectField("keyframes", keys);
    }

    static void PasteKeyframes(AnimationClip clip, float timeOffset) {
        // 从剪贴板读取并插入关键帧
        Keyframe[] keys = (Keyframe[])EditorUtility.GetObjectField("keyframes");
        if (keys != null) {
            AnimationKeyframe[] newKeys = new AnimationKeyframe[keys.Length];
            for (int i = 0; i < keys.Length; i++) {
                newKeys[i] = new AnimationKeyframe {
                    time = keys[i].time + timeOffset,
                    value = keys[i].value
                };
            }
            clip.SetKeyframes(newKeys);
        }
    }
}