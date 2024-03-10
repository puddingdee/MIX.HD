<Cabbage>
form caption("gain")
rslider channel("gain1"), text("vox fader"), range(0, 1, 0.7)

form caption("gain")
rslider channel("gain2"), text("bass fader"), range(0, 1, 0.7)

form caption("gain")
rslider channel("gain3"), text("lead fader"), range(0, 1, 0.7)

form caption("gain")
rslider channel("gain4"), text("drum fader"), range(0, 1, 0.7)



;instr 1
form caption("distort")
rslider channel("dist1"), text("distort1"), range(0, 2, 0)


form caption("exciter")
checkbox channel("ex1")



form caption("fold")
rslider channel("fold1"), text("fold amt1"), range(0, 100, 0)

form caption("delayt")
rslider channel("dlt1"), text("delay time1"), range(1, 500, 1)

form caption("delaym")
checkbox channel("dlm1")

form caption("butterlp")
rslider channel("cut1"), text("lp cutoff1"), range(10000, 20000, 20000)

;instr 2
form caption("distort")
rslider channel("dist2"), text("distort2"), range(0, 2, 0)

form caption("exciter")
checkbox channel("ex2")



form caption("fold")
rslider channel("fold2"), text("fold amt2"), range(0, 100, 0)

form caption("delayt")
rslider channel("dlt2"), text("delay time2"), range(1, 500, 1)

form caption("delaym")
checkbox channel("dlm2")

form caption("butterlp")
rslider channel("cut2"), text("lp cutoff2"), range(10000, 20000, 20000)

;instr 3
form caption("distort")
rslider channel("dist3"), text("distort3"), range(0, 2, 0)

form caption("exciter")
checkbox channel("ex3")



form caption("fold")
rslider channel("fold3"), text("fold amt3"), range(0, 100, 0)

form caption("delayt")
rslider channel("dlt3"), text("delay time3"), range(1, 500, 1)

form caption("delaym")
checkbox channel("dlm3")

form caption("butterlp")
rslider channel("cut3"), text("lp cutoff3"), range(10000, 20000, 20000)

;instr 4
form caption("distort")
rslider channel("dist4"), text("distort4"), range(0, 2, 0)

form caption("exciter")
checkbox channel("ex4")



form caption("fold")
rslider channel("fold4"), text("fold amt4"), range(0, 100, 0)

form caption("delayt")
rslider channel("dlt4"), text("delay time4"), range(1, 500, 1)

form caption("delaym")
checkbox channel("dlm4")

form caption("butterlp")
rslider channel("cut4"), text("lp cutoff4"), range(10000, 20000, 20000)

;master
form caption("clip")
checkbox channel("clip")

form caption("cross")
checkbox channel("cross")



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




instr 1

;initialize all instrument variables
ichnls = ftchnls(p4)



kgain1 chnget "gain1"
kex chnget "ex1"
kdist chnget "dist1"
kdistgain = (kdist*1.5)+1
kfold chnget "fold1"
kdlt chnget "dlt1"
kdlm chnget "dlm1"

kcutoff chnget "cut1"
kportTime linseg 0, 0.001, 0.01

kn = kcutoff/(20000/(logbtwo(20000)))

kcutport portk 2^kn, kportTime

kdlp portk kdlt, kportTime



gifn	ftgen	0,0, 257, 9, .5,1,270

;assign file to channels
if (ichnls == 1) then
    a1 loscil  0.7, 1, p4, 1
    a2 = a1
elseif (ichnls == 2) then
    a1, a2 loscil  0.7, 1, p4, 1
else
    a1 = 0
    a2 = 0
endif


;channel 1 fx
adist1 distort a1*kdistgain, kdist, gifn
adist2 distort a2*kdistgain, kdist, gifn




afold1 fold adist1, kfold
afold2 fold adist2, kfold

aex1 exciter adist1, 5000, 20000, 10, 2
aex2 exciter adist2, 5000, 20000, 10, 2

adel1 vdelay afold1, kdlp, 2000
adel2 vdelay afold2, kdlp, 2000


if (kex == 1 && kdlm == 1) then
    aLPfilt1 butterlp afold1+(aex1*10)+adel1, kcutport
    aLPfilt2 butterlp afold2+(aex2*10)+adel2, kcutport
elseif (kex == 1) then
    aLPfilt1 butterlp afold1+(aex1*10), kcutport
    aLPfilt2 butterlp afold2+(aex2*10), kcutport
elseif (kdlm == 1) then
    aLPfilt1 butterlp afold1+adel1, kcutport
    aLPfilt2 butterlp afold2+adel2, kcutport
else
    aLPfilt1 butterlp afold1, kcutport
    aLPfilt2 butterlp afold2, kcutport
endif

;global mix1
gamix1 = aLPfilt1*kgain1
gamix2 = aLPfilt2*kgain1


endin

instr 2

;initialize all instrument variables

ichnls = ftchnls(p4)



kgain1 chnget "gain2"
kex chnget "ex2"
kdist chnget "dist2"
kdistgain = (kdist*1.5)+1
kfold chnget "fold2"
kdlt chnget "dlt2"
kdlm chnget "dlm2"

kcutoff chnget "cut2"
kportTime linseg 0, 0.001, 0.01

kn = kcutoff/(20000/(logbtwo(20000)))

kcutport portk 2^kn, kportTime

kdlp portk kdlt, kportTime





;assign file to channels
if (ichnls == 1) then
    a1 loscil  0.7, 1, p4, 1
    a2 = a1
elseif (ichnls == 2) then
    a1, a2 loscil  0.7, 1, p4, 1
else
    a1 = 0
    a2 = 0
endif


;channel 1 fx
adist1 distort a1*kdistgain, kdist, gifn
adist2 distort a2*kdistgain, kdist, gifn




afold1 fold adist1, kfold
afold2 fold adist2, kfold

aex1 exciter adist1, 5000, 20000, 10, 2
aex2 exciter adist2, 5000, 20000, 10, 2

adel1 vdelay afold1, kdlp, 2000
adel2 vdelay afold2, kdlp, 2000


if (kex == 1 && kdlm == 1) then
    aLPfilt1 butterlp afold1+(aex1*10)+adel1, kcutport
    aLPfilt2 butterlp afold2+(aex2*10)+adel2, kcutport
elseif (kex == 1) then
    aLPfilt1 butterlp afold1+(aex1*10), kcutport
    aLPfilt2 butterlp afold2+(aex2*10), kcutport
elseif (kdlm == 1) then
    aLPfilt1 butterlp afold1+adel1, kcutport
    aLPfilt2 butterlp afold2+adel2, kcutport
else
    aLPfilt1 butterlp afold1, kcutport
    aLPfilt2 butterlp afold2, kcutport
endif

;global mix2
gamix3 = aLPfilt1*kgain1
gamix4 = aLPfilt2*kgain1


endin

instr 3

;initialize all instrument variables

ichnls = ftchnls(p4)


kgain1 chnget "gain3"
kex chnget "ex3"
kdist chnget "dist3"
kdistgain = (kdist*1.5)+1
kfold chnget "fold3"
kdlt chnget "dlt3"
kdlm chnget "dlm3"

kcutoff chnget "cut3"
kportTime linseg 0, 0.001, 0.01

kn = kcutoff/(20000/(logbtwo(20000)))

kcutport portk 2^kn, kportTime

kdlp portk kdlt, kportTime




;assign file to channels
if (ichnls == 1) then
    a1 loscil  0.7, 1, p4, 1
    a2 = a1
elseif (ichnls == 2) then
    a1, a2 loscil  0.7, 1, p4, 1
else
    a1 = 0
    a2 = 0
endif


;channel 4 fx
adist1 distort a1*kdistgain, kdist, gifn
adist2 distort a2*kdistgain, kdist, gifn




afold1 fold adist1, kfold
afold2 fold adist2, kfold

aex1 exciter adist1, 5000, 20000, 10, 2
aex2 exciter adist2, 5000, 20000, 10, 2

adel1 vdelay afold1, kdlp, 2000
adel2 vdelay afold2, kdlp, 2000



if (kex == 1 && kdlm == 1) then
    aLPfilt1 butterlp afold1+(aex1*10)+adel1, kcutport
    aLPfilt2 butterlp afold2+(aex2*10)+adel2, kcutport
elseif (kex == 1) then
    aLPfilt1 butterlp afold1+(aex1*10), kcutport
    aLPfilt2 butterlp afold2+(aex2*10), kcutport
elseif (kdlm == 1) then
    aLPfilt1 butterlp afold1+adel1, kcutport
    aLPfilt2 butterlp afold2+adel2, kcutport
else
    aLPfilt1 butterlp afold1, kcutport
    aLPfilt2 butterlp afold2, kcutport
endif

;global mix3
gamix5 = aLPfilt1*kgain1
gamix6 = aLPfilt2*kgain1


endin

instr 4

;initialize all instrument variables

ichnls = ftchnls(p4)




kgain1 chnget "gain4"
kex chnget "ex4"
kdist chnget "dist4"
kdistgain = (kdist*1.5)+1
kfold chnget "fold4"
kdlt chnget "dlt4"
kdlm chnget "dlm4"

kcutoff chnget "cut4"
kportTime linseg 0, 0.001, 0.01

kn = kcutoff/(20000/(logbtwo(20000)))

kcutport portk 2^kn, kportTime

kdlp portk kdlt, kportTime




;assign file to channels
if (ichnls == 1) then
    a1 loscil  0.7, 1, p4, 1
    a2 = a1
elseif (ichnls == 2) then
    a1, a2 loscil  0.7, 1, p4, 1
else
    a1 = 0
    a2 = 0
endif


;channel 4 fx
adist1 distort a1*kdistgain, kdist, gifn
adist2 distort a2*kdistgain, kdist, gifn




afold1 fold adist1, kfold
afold2 fold adist2, kfold

aex1 exciter adist1, 5000, 20000, 10, 2
aex2 exciter adist2, 5000, 20000, 10, 2

adel1 vdelay afold1, kdlp, 2000
adel2 vdelay afold2, kdlp, 2000


if (kex == 1 && kdlm == 1) then
    aLPfilt1 butterlp afold1+(aex1*10)+adel1, kcutport
    aLPfilt2 butterlp afold2+(aex2*10)+adel2, kcutport
elseif (kex == 1) then
    aLPfilt1 butterlp afold1+(aex1*10), kcutport
    aLPfilt2 butterlp afold2+(aex2*10), kcutport
elseif (kdlm == 1) then
    aLPfilt1 butterlp afold1+adel1, kcutport
    aLPfilt2 butterlp afold2+adel2, kcutport
else
    aLPfilt1 butterlp afold1, kcutport
    aLPfilt2 butterlp afold2, kcutport
endif

;global mix4
gamix7 = aLPfilt1*kgain1
gamix8 = aLPfilt2*kgain1


endin

instr 5
kclip chnget "clip"
kcross chnget "cross"
kestfreq = 500
kmaxvar = 1


amasterin1 = gamix1 + gamix3 + gamix5 + gamix7
amasterin2 = gamix2 + gamix4 + gamix6 + gamix8

;master fx
across1 cross2 amasterin1, gamix7, 4096, 2, 5, 1
across2 cross2 amasterin2, gamix8, 4096, 2, 5, 1

aharm1 harmon across1, kestfreq, kmaxvar, kestfreq*0.5, kestfreq*2, 1, 80, 0.2
aharm2 harmon across2, kestfreq, kmaxvar, kestfreq*0.5, kestfreq*2, 1, 80, 0.2

;saturation dist (adjustable mix)
if (kcross == 1) then
    aclip1 clip aharm1, 1, 0.2
    aclip2 clip aharm2, 1, 0.2
else
    aclip1 clip amasterin1, 1, 0.2
    aclip2 clip amasterin2, 1, 0.2
endif

;compressor
if (kclip == 1) then
    acomp1 compress aclip1, aclip1, 0, 48, 60, 4, 0.1, 0.5, 0.02
    acomp2 compress aclip2, aclip2, 0, 48, 60, 4, 0.1, 0.5, 0.02
elseif (kcross == 1) then
    acomp1 compress aharm1, aharm1, 0, 48, 60, 4, 0.1, 0.5, 0.02
    acomp2 compress aharm2, aharm2, 0, 48, 60, 4, 0.1, 0.5, 0.02
else
    acomp1 compress amasterin1, amasterin1, 0, 48, 60, 4, 0.1, 0.5, 0.02
    acomp2 compress amasterin2, amasterin2, 0, 48, 60, 4, 0.1, 0.5, 0.02
endif

amasterout1 = acomp1
amasterout2 = acomp2
    outs amasterout1*9, amasterout2*9
endin


</CsInstruments>
<CsScore>
f 1  0 0    1   "o_vox_1.mp3" 0 0 0
f 2  0 0    1   "o_bass_1.mp3" 0 0 0
f 3  0 0    1   "o_lead.mp3" 0 0 0
f 4  0 0    1   "o_drums_1.mp3" 0 0 0

f 5 0 2048 20 2	;windowing function


i 1 0 135 1
i 2 0 135 2
i 3 0 135 3
i 4 0 135 4
i 5 0 135
e
</CsScore>
</CsoundSynthesizer>