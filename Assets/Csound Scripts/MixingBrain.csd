<Cabbage>
form caption("gain")
rslider channel("gain1"), text("vox fader"), range(0, 1, 0.7)


form caption("gain")
rslider channel("gain2"), text("drum fader"), range(0, 1, 0.7)

form caption("distort")
rslider channel("dist"), text("distort"), range(0, 2, 0)

form caption("exciter")
rslider channel("ex1"), text("exciter mix"), range(0, 25, 0)

form caption("mirrorL")
rslider channel("mirL"), text("mirror dist"), range(-1, 0.8, -1)

form caption("fold")
rslider channel("fold"), text("fold amt"), range(0, 100, 0)

form caption("delayt")
rslider channel("dlt"), text("delay time"), range(1, 500, 1)

form caption("delaym")
rslider channel("dlm"), text("delay mix"), range(0, 1, 0)


form caption("butterlp")
rslider channel("cut"), text("lp cutoff"), range(10000, 20000, 20000)


</Cabbage>

<CsoundSynthesizer>
<CsOptions>
-n -d 
-odac
</CsOptions>
<CsInstruments>
; Initialize the global variables. 
sr = 44100
ksmps = 32
nchnls = 2
0dbfs = 1

instr 99


;initialize all instrument variables
ichnls = ftchnls(p4)
ichnls2 = ftchnls(p5)
ichnls3 = ftchnls(p6)
ichnls4 = ftchnls(p7)
prints  "\nnumber of channels = %d\n\n", ichnls

kgain chnget "gain1"
kgain2 chnget "gain2"
kex chnget "ex1"
klow chnget "mirL"
khigh chnget "mirH"
kdist chnget "dist"
kpregain chnget "pregain"
kshape1 chnget "shape1"
kfold chnget "fold"
kdlt chnget "dlt"
kdlm chnget "dlm"


kcutoff chnget "cut"
kportTime linseg 0, 0.001, 0.01

kn = kcutoff/(20000/(logbtwo(20000)))

kcutport portk 2^kn, kportTime

kdlp portk kdlt, kportTime
kmirp portk klow, kportTime


gifn	ftgen	0,0, 257, 9, .5,1,270

;assign all channels
if (ichnls == 1) then
    a1 loscil  1, 1, p4, 1
    a2 = a1
elseif (ichnls == 2) then
    a1, a2 loscil  1, 1, p4, 1
else
    a1 = 0
    a2 = 0
endif

if (ichnls2 == 1) then
    a3 loscil  kgain2, 1, p5, 1
    a4 = a3
elseif (ichnls2 == 2) then
    a3, a4 loscil  1, 1, p5, 1
else
    a3 = 0
    a4 = 0
endif

if (ichnls2 == 1) then
    a5 loscil  1, 1, p6, 1
    a6 = a5
elseif (ichnls2 == 2) then
    a5, a6 loscil  1, 1, p6, 1
else
    a5 = 0
    a6 = 0
endif

if (ichnls2 == 1) then
    a7 loscil  1, 1, p7, 1
    a8 = a7
elseif (ichnls2 == 2) then
    a7, a8 loscil  1, 1, p7, 1
else
    a7 = 0
    a8 = 0
endif


;channel 1 fx
adist1 distort a1, kdist, gifn
adist2 distort a2, kdist, gifn


amir1 mirror adist1, kmirp, 1
amir2 mirror adist2, kmirp, 1

afold1 fold amir1, kfold
afold2 fold amir2, kfold

aex1 exciter a1, 2200, 20000, 10, 2
aex2 exciter a2, 2200, 20000, 10, 2

adel1 vdelay afold1, kdlp, 2000
adel2 vdelay afold2, kdlp, 2000



aLPfilt1 butterlp afold1+(aex1*kex)+(adel1*kdlm), kcutport
aLPfilt2 butterlp afold2+(aex2*kex)+(adel2*kdlm), kcutport

;mix1
amix1 = aLPfilt1
amix2 = aLPfilt2

;channel 2 fx







;master fx
;saturation dist (adjustable mix)
;compressor
;limiter

;put it all together(?) move kgain down here
amaster1 = amix1 + a3
amaster2 = amix2 + a4

    outs amix1, amix2
endin




</CsInstruments>
<CsScore>
f 1  0 0    1   "o_vox_1.mp3" 0 0 0
f 2  0 0    1   "o_drums_1.mp3" 0 0 0
f 3  0 0    1   "o_drums_1.mp3" 0 0 0
f 4  0 0    1   "o_drums_1.mp3" 0 0 0


i 99 0 135 1 2 3 4
e
</CsScore>
</CsoundSynthesizer>