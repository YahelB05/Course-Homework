# ---------------------------------- Imports ------------------------------------
import math


# ---------------------------------- Helper Functions ---------------------------
def num_len(num: int) -> int:
    return int(math.log10(num)) + 1


# ---------------------------------- Main ---------------------------------------
def main() -> None:
    test_inputs = [1, 22, 333, 4444]
    for test_input in test_inputs:
        print(f'{test_input} -> {num_len(test_input)}')


if __name__ == '__main__':
    main()
