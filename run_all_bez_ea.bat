@echo off

echo Podaj ile Voterow potrzebujesz:


set/p "Voter=>>"



for /L %%A in (1,1,%Voter%) do (
	START Voter\Voter\bin\Debug\Voter.exe
	)


START Proxy\Proxy\bin\Debug\Proxy.exe


end
