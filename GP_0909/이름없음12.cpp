#include <stdio.h>
#include <math.h>
#include <windows.h>
#include <conio.h>

int calc_frequency(int octave, int inx);
void practice_piano(void);

int main(void)
{
	practice_piano();
	return 0;
}

void practice_piano(void)
{
	int index[] = { 0,2,4,5,7,9,11,12 };
	int freq[8],code;

	for (int i = 0; i < 8; i++)
		freq[i] = calc_frequency(4, index[i]);

	do
	{
		code = getch();
		if ('1' <= code && code <= '8')
		{
			code -= 49;
			Beep(freq[code], 300);
		}
	} while (code != 27);
}

int calc_frequency(int octave, int inx)
{
	double do_scale = 32.7032;
	double ratio = pow(2., 1 / 12.), temp;

	temp = do_scale * pow(2, octave - 1);
	for (int i = 0; i < inx; i++)
	{
		temp = (int)(temp + 0.5);
		temp *= ratio;
	}

	return (int)temp;
}


