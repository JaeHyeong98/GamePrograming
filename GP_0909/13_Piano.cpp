#include <stdio.h>
#include <math.h>
#include <windows.h>

void print_frequency(int octave);
int calc_frequency(int octave, int inx);

int main(void)
{
	int index[] = { 0,2,4,5,7,9,11,12 };
	int freq[8];
	char *scale[] = { "도","도#","레","레#", "미", "파","파#","솔","솔#","라","라#","시","도" };

	for (int i = 0; i < 8; i++)
		freq[i] = calc_frequency(4, index[i]);

	for (int i = 0; i <= 7; i++)
		Beep(freq[i], 500);

	Sleep(1000);

	for (int i = 7; i >=0 ; i--)
	{
		Beep(freq[i], 500);
	}

	int octave, count = 0;
	printf("음계와 주파수\n\n음계\t    ");

	for (int i = 0; i < 12; i++)
		printf("%-5s", scale[i]);
	printf("\n");

	for (int i = 0; i <= 70; i++)
		printf("-");
	printf("\n");

	for (octave = 1; octave < 7; octave++)
	{
		print_frequency(octave);
	}

	return 0;
}

void print_frequency(int octave)
{
	double do_scale = 32.7032;
	double ratio = pow(2., 1 / 12.), temp;

	temp = do_scale * pow(2, octave - 1);
	printf("%d옥타브 : ", octave);

	for (int i = 0; i < 12; i++)
	{
		printf("%4ld ", (unsigned long)(temp + 0.5));
		temp *= ratio;
	}
	printf("\n");
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
