@echo off

echo Podaj ile Voterow potrzebujesz:


set/p "Voter=>>"



for /L %%A in (1,1,%Voter%) do (
	START Voter\Voter\bin\Debug\Voter.exe
	)


START ElectionAuthority\ElectionAuthority\bin\Debug\ElectionAuthority.exe


end
