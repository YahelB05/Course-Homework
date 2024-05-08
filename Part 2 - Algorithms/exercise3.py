# ---------------------------------- Helper Functions ---------------------------
def is_sorted_string(string: str) -> bool:
    return string == ''.join(sorted(string))


def is_polyndrom(string: str) -> bool:
    # a polyndrom must contain characters, and have an odd amount of characters.
    if not string or len(string) % 2 == 0:
        return False

    return string[0:len(string) // 2] == string[:len(string) // 2:-1]


def is_sorted_polyndrom(string: str) -> bool:
    return is_polyndrom(string) and is_sorted_string(string[0:len(string) // 2 + 1])


# ---------------------------------- Main ---------------------------------------
def main() -> None:
    test_inputs = ['שוש', 'אבגדגבא', 'abcdcba', '12321', '45421']
    for test_input in test_inputs:
        print(f'{test_input} -> {is_sorted_polyndrom(test_input)}')


if __name__ == '__main__':
    main()
