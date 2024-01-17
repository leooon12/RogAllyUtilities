# RogAllyUtilities - utility apps for ROG Ally

I made a couple of utility apps for my ROG Ally that I want to share.

[Download](https://github.com/leooon12/RogAllyUtilities/releases/latest)

## PowerControl

Enforces specified Windows Power Mode even when switching Armory Crate Operating Modes.

After the first launch, a Configuration.json file will appear next to the executable file with two values:
- CurrentPowerMode - last chosen Power Mode
- PollingInterval - determines how often the app checks current Power Mode

As for me, I personally always use Balanced Power Mode.

<img width="345" alt="PowerControl" src="https://github.com/leooon12/RogAllyUtilities/assets/4081669/3980dea5-3f8e-430d-8a09-10963b3dfe53">

## GyroRestart

I found out that gyro does not work after waking up from hibernate. To fix this, you have to disable/enable Bosch Accelerometer. That’s exactly what GyroRestart does.

You need to run GyroRestart with Administrator Privileges as they are required for disabling / enabling devices.

<img width="448" alt="GytoRestart" src="https://github.com/leooon12/RogAllyUtilities/assets/4081669/c7872cf8-8305-4fa1-8c49-13c2c9afa0b7">

---
### Credits

- I used icons made by [Freepik](https://www.flaticon.com/authors/freepik)
- I checked a couple of things in [G-Helper](https://github.com/seerge/g-helper) source code
- The last part of this README is also from [G-Helper](https://github.com/seerge/g-helper)

---

Disclaimers: “ROG Ally” and “Armoury Crate” are trademarked by and belong to AsusTek Computer, Inc. I make no claims to these or any assets belonging to AsusTek Computer and use them purely for informational purposes only.

THE SOFTWARE IS PROVIDED “AS IS” AND WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. MISUSE OF THIS SOFTWARE COULD CAUSE SYSTEM INSTABILITY OR MALFUNCTION.
