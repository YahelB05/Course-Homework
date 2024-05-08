# ---------------------------------- Imports ------------------------------------
import matplotlib.pyplot as plt
import scipy

# ---------------------------------- Constants ----------------------------------
LOOP_BREAK_INPUT = -1


# ---------------------------------- Helper Functions ---------------------------
def avg(numbers: list) -> float:
    return sum(numbers) / len(numbers)


def count_positives(numbers: list) -> int:
    return len(list(filter(lambda num: num > 0, numbers)))


def display_graph(numbers: list) -> None:
    plt.plot(numbers, 'ro')
    plt.xlabel("Input Index")
    plt.ylabel("User Input")
    
    plt.show()


# ---------------------------------- Main ---------------------------------------
def main() -> None:
    numbers = []
    while True:
        num = float(input('Enter a Number: '))
        if num == LOOP_BREAK_INPUT:
            break
        numbers.append(num)

    print(f'Avg: {avg(numbers)}')
    print(f'Positives Count: {count_positives(numbers)}')
    print(f'Ascending Order: {sorted(numbers)}')
    print(f'Pearson Correlation Coefficient: {scipy.stats.pearsonr(numbers, list(range(len(numbers))))[0]}')
    display_graph(numbers)


if __name__ == '__main__':
    main()
