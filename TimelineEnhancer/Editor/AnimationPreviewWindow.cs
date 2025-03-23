using UnityEditor;
using UnityEngine;

public class AnimationPreviewWindow : EditorWindow
{
    [MenuItem("Tools/Timeline Enhancer/Animation Preview")]
    public static void ShowWindow()
    {
        GetWindow<AnimationPreviewWindow>("Animation Preview");
    }

    private AnimationPlayer player;
    private float playbackSpeed = 1f;
    private bool isLooping = false;
    private bool isMuted = false;
    private AnimationClip currentClip;

    private void OnGUI()
    {
        GUILayout.Label("Animation Preview", EditorStyles.boldLabel);

        // 动画选择区域
        if (GUILayout.Button("Load Animation", GUILayout.Width(120)))
        {
            currentClip = Selection.activeObject as AnimationClip;
            if (currentClip == null)
            {
                Debug.LogWarning("Please select an AnimationClip in the Project window");
                return;
            }
        }

        // 播放控制
        if (currentClip != null)
        {
            if (GUILayout.Button(player.isPlaying ? "Pause" : "Play", GUILayout.Width(80)))
            {
                TogglePlay();
            }

            // 播放速度调节
            GUILayout.BeginHorizontal();
            GUILayout.Label("Speed: ", GUILayout.Width(60));
            playbackSpeed = Mathf.Clamp(GUILayout.HorizontalSlider(playbackSpeed, 0.1f, 5f), 0.1f, 5f);
            GUILayout.EndHorizontal();

            // 循环模式
            GUILayout.BeginHorizontal();
            isLooping = GUILayout.Toggle(isLooping, "Loop");
            GUILayout.EndHorizontal();

            // 静音模式
            GUILayout.BeginHorizontal();
            isMuted = GUILayout.Toggle(isMuted, "Mute");
            GUILayout.EndHorizontal();
        }
        else
        {
            GUILayout.Label("No animation selected", EditorStyles.boldLabel);
        }

        // 状态显示
        if (player.isPlaying)
        {
            GUILayout.Label($"Playing: {currentClip.name} ({playbackSpeed}x)", GUILayout.Width(200));
        }
    }

    private void TogglePlay()
    {
        if (player == null || currentClip == null)
        {
            InitializePlayer();
        }

        if (player.isPlaying)
        {
            player.Stop();
        }
        else
        {
            player.Play();
        }
    }

    private void InitializePlayer()
    {
        // 创建临时游戏对象用于动画播放
        GameObject previewObject = new GameObject("AnimationPreviewTemp");
        player = previewObject.AddComponent<AnimationPlayer>();

        // 设置动画参数
        player.clip = currentClip;
        player.speed = playbackSpeed;
        player.loop = isLooping;
        
        // 静音处理
        if (isMuted)
        {
            AudioUtil.SetMasterMute(true);
        }

        // 添加场景对象
        SceneManager.MoveSceneAdditive(previewObject.name);
    }

    private void OnDisable()
    {
        // 清理临时对象
        if (player != null)
        {
            DestroyImmediate(player.gameObject);
            player = null;
        }

        // 恢复音频设置
        if (isMuted)
        {
            AudioUtil.SetMasterMute(false);
            isMuted = false;
        }
    }

    // 静音工具类（Unity 2022+ 推荐使用）
    private static class AudioUtil
    {
        public static void SetMasterMute(bool mute)
        {
            AudioManager.SetMasterMute(mute);
        }
    }
}