(define (sqrt-iter guess x improve transform)
	(if (good-enough? transform guess x)
		guess
		(sqrt-iter (improve guess x)
			x improve transform)))

(define (improve-sqrt guess x)
	(average guess (/ x guess)))

(define (improve-cube guess x)
	(/ (+ (/ x (sqr guess)) (* 2 guess)) 3))

(define (average x y)
	(/ (+ x y) 2))

(define (good-enough? transform guess x)
	(< (abs (- (transform guess) x)) 0.00001))

(define (abs x) (if (< x 0) (- x) x))

(define (sqr x) (* x x))

(define (cube x) (* x x x))

(define (sqrt x)
	(sqrt-iter 1.0 x improve-sqrt sqr))

(define (cube-root x)
	(sqrt-iter 1.0 x improve-cube cube))