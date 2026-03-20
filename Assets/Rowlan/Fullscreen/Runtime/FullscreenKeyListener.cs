using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Rowlan.Fullscreen
{
    /// <summary>
    /// Invisible MonoBehaviour that catches keybind input when the Game View has focus
    /// during Play Mode. Spawned automatically by FullscreenGameView when Play Mode starts.
    ///
    /// Reads keybinds from FullscreenKeybinds (populated by the editor at spawn time).
    /// Communicates back to the editor via static delegates to avoid a compile-time
    /// dependency on the Editor assembly.
    /// </summary>
    public class FullscreenKeyListener : MonoBehaviour
    {
        #region Editor Callbacks

        /// <summary>
        /// Delegate invoked when the toggle or exit key is pressed.
        /// Assigned by FullscreenGameView (editor side) before this component is spawned.
        /// </summary>
        public static Action OnTogglePressed;

        /// <summary>
        /// Delegate invoked to check whether fullscreen is currently active.
        /// Assigned by FullscreenGameView (editor side) before this component is spawned.
        /// </summary>
        public static Func<bool> IsFullscreen;

        #endregion

        #region Cached Keybinds

        private Key toggleKey;
        private Key exitKey;
        private bool keybindsValid;

        #endregion

        #region Initialization

        /// <summary>
        /// Caches the current keybind settings at spawn time and validates them.
        /// If either key is invalid the component disables itself entirely.
        /// </summary>
        private void Awake()
        {
            toggleKey = FullscreenKeybinds.ToggleKey;
            exitKey = FullscreenKeybinds.ExitKey;

            if (Keyboard.current == null)
            {
                Debug.LogWarning("[Fullscreen] No keyboard device found. Key listener disabled.");
                enabled = false;
                return;
            }

            keybindsValid = IsValidKey(toggleKey) && IsValidKey(exitKey);

            if (!keybindsValid)
            {
                Debug.LogWarning($"[Fullscreen] Invalid keybinds (toggle='{toggleKey}', exit='{exitKey}'). Key listener disabled.");
                enabled = false;
            }
        }

        /// <summary>
        /// Returns true if the key resolves to a valid KeyControl via the Keyboard indexer.
        /// </summary>
        private static bool IsValidKey(Key key)
        {
            if (Keyboard.current == null) return false;

            try
            {
                return Keyboard.current[key] != null;
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }
        }

        #endregion

        #region Input Handling

        /// <summary>
        /// Checks for toggle and exit key presses each frame
        /// and forwards them to the editor via the delegate.
        /// </summary>
        private void Update()
        {
            if (Keyboard.current == null) return;

            if (Keyboard.current[toggleKey].wasPressedThisFrame)
            {
                OnTogglePressed?.Invoke();
            }
            else if (Keyboard.current[exitKey].wasPressedThisFrame)
            {
                if (IsFullscreen != null && IsFullscreen())
                    OnTogglePressed?.Invoke();
            }
        }

        #endregion
    }
}
