# Interactive Height-Field Water Simulation

A real-time interactive water ripple system implemented in **Unity Built-in Render Pipeline**.  
This project simulates 2D wave propagation using GPU Ping-Pong Buffers.

## 🎥 Demo
<img width="800" height="810" alt="QQ20260618-155719-ezgif com-video-to-gif-converter (1)" src="https://github.com/user-attachments/assets/9f955dfe-43da-44e7-9368-cf8f0263898c" />


## 🚀 Features
- **Real-time Interaction**: Click or drag on the water surface to generate ripples.
- **GPU Accelerated**: Uses `RenderTexture` double buffering for high-performance computation.
- **Stable Simulation**: Fixed numerical overflow issues (white edges) with value clamping.

## 🛠 Technical Details

### Core Algorithm
The wave propagation is based on a simplified 2D wave equation:

`h_new = (h_left + h_right + h_up + h_down) / 2 - h_old`

Where:
- `h_new`: The height of the current pixel in the next frame.
- `h_left/right/up/down`: The heights of the neighboring pixels.
- `h_old`: The height of the current pixel in the previous frame.

### Architecture
1. **C# Script**: Handles mouse input and RenderTexture swapping (Ping-Pong).
2. **Ripple Shader**: Computes wave physics on the GPU.
3. **Draw Shader**: Injects disturbances (mouse clicks) into the height field.
4. **ASE**: Converts the height map into a normal map for lighting.

## 📦 How to Run
1. Clone this repo.
2. Open with Unity (Built-in RP).
3. Make sure **Amplify Shader Editor** is installed.
4. Open `SampleScene` and press Play.

## 📧 Contact
- GitHub: [@zenggenb666](https://github.com/zenggenb666)
