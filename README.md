Funkcionalno programiranje (FP)
===============================

## [Literatura](./Literatura/)
- [Functional Programming Using F#](./Literatura/Functional_Programming_Using_F#.pdf)
- [Expert F# 4.0](./Literatura/Expert_F#_4.0.pdf)

## [Predavanja](./Predavanja)
Bilješke sa predavanja.

## [Prezentacije](./Prezentacije)
Profesorove prezentacije iz predmeta.

## [Vježbe](./Vjezbe)
Kod i primjeri sa vježbi.

## [Random](./Random)
Random zadaci koje sam smislio dok sam vježbao.

## [Zadaće](./Zadace)
Zadaće zadane u toku semestra.
- #### [Zadaća 1](./Zadace/Zadaca1/)
    - [Movie Management Database](./Zadace/Zadaca1/Zadatak1/)
    - [Calculator](./Zadace/Zadaca1/Zadatak2/)

### [Skripta za instalaciju/update okruženja](./getfsshell.sh)
Za pokretanje skripte koristi se komanda `. ./getfsshell.sh` pri čemu je potrebno biti u direktoriji gdje se nalazi `getfsshell.sh`.
Skripta automatski skine najnoviju verziju okruženja, odradi sve što je potrebno i vrati terminal u direktoriju u kojoj je i bio.

### Komanda za brisanje NIX cache
Ukoliko se skine okruženje i u nekog kraćem periodu (npr. 1 dan) izađe nova verzija okruženja neće biti moguće preuzeti novu verziju dok ne prođe taj period.
Čekanje se može zaobići brisanjem cache-a komandom `rm -rf ~/.cache/nix/`.
