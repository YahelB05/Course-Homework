# ---------------------------------- Helper Functions ---------------------------
def is_pythagorean_triplet(a: int, b: int, c: int) -> bool:
    return a < b < c and (a ** 2) + (b ** 2) == (c ** 2)


def pythagorean_triplet_by_sum(sum: int) -> None:
    for a in range(3, sum // 3):
        for b in range(a + 1, sum // 2):
            c = sum - b - a
            if is_pythagorean_triplet(a, b, c):
                print(f'{a} < {b} < {c}')
                return  # Each pythagorean triplet has a unique sum. Thus, when one is found - we can exit the function.


# ---------------------------------- Main ---------------------------------------
def main() -> None:
    test_inputs = [12, 30, 306]
    for test_input in test_inputs:
        pythagorean_triplet_by_sum(test_input)


if __name__ == '__main__':
    main()
