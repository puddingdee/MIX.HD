<Cabbage>
form caption("SimpleFreq")
rslider channel("freq1"), text("Frequency Slider"), range(0, 10000, 0)
form caption("SimpleGain")
rslider channel("gain1"), text("Gain Fader"), range(0, 1, 0)
</Cabbage>
<CsoundSynthesizer>
<CsOptions>
-n -d 
-odac
</CsOptions>
<CsInstruments>
; Initialize the global variables. 
ksmps = 32
nchnls = 2
0dbfs = 1

instr 99
idur = p3
iamp = ampdbfs(p4)
icps = p5

kcps chnget "freq1"
kamp chnget "gain1"

ahello vco2 kamp, kcps, 10
    out ahello, ahello
endin

</CsInstruments>
<CsScore>
i 99 0 100 0 400
</CsScore>
</CsoundSynthesizer>