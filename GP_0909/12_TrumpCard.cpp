#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <time.h>

struct  trump
{
	char order;
	char shape[3];
	char number;
};

void make_card(trump m_card[]);

void display_card(trump m_card[]);

void shuffle_card(trump m_card[]);

int main(void)
{
	trump card[52];
	make_card(card);
	//shuffle_card(card);
	display_card(card);
	
	return 0;
}

void make_card(trump m_card[])
{
	char shape[4][3] = { "��","��","��","��" };

	for (int i = 0; i < 4; i++)
	{
		for (int j = i * 13; j < i * 13+13; j++)
		{
			m_card[j].order = i;
			strcpy(m_card[j].shape, shape[i]);
			m_card[j].number = j % 13 + 1;
			switch (m_card[j].number)
			{
			case 1:
				m_card[j].number = 'A';
				break;

			case 11:
				m_card[j].number = 'J';
				break;

			case 12:
				m_card[j].number = 'Q';
				break;

			case 13:
				m_card[j].number = 'K';
				break;
			}
		}
	}
}

void display_card(trump m_card[])
{
	int count = 0;
	for (int i = 0; i < 52; i++)
	{
		printf("%s", m_card[i].shape);
		if (10 < m_card[i].number)
		{
			printf("%-2c ", m_card[i].number);
		}
		else
		{
			printf("%-2d ", m_card[i].number);
		}
		
		count++;
		if (i % 13 + 1 == 13)
		{
			printf("\n");
			count = 0;
		}
	}
}

void shuffle_card(trump m_card[])
{
	int rnd;
	trump temp;
	srand(time(NULL));
	for (int i = 0; i < 52; i++)
	{
		rnd = rand() % 52;
		temp = m_card[rnd];
		m_card[rnd] = m_card[i];
		m_card[i] = temp;
	}
}