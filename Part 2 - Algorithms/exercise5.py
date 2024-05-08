# ---------------------------------- Imports ------------------------------------
from mpmath import mp


# ---------------------------------- Helper Functions ---------------------------
def reverse_n_pi_digits(n: int) -> str:
    mp.dps = n
    pi_str = str(mp.pi).replace('.', '')  # removes the decimal point - because it's not a digit.
    return pi_str[n-1::-1]


# ---------------------------------- Main ---------------------------------------
def main() -> None:
    print(reverse_n_pi_digits(5))


if __name__ == '__main__':
    main()
